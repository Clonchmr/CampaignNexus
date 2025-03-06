import { useContext, useEffect, useState } from "react";
import { Button, Col, Container, Form, Modal, Row } from "react-bootstrap";
import { ThemeContext } from "../../../ThemeContext/ThemeContext";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  getCharacterById,
  levelUpCharacter,
} from "../../../managers/characterManager";

export const CharacterLevelUpModal = ({
  modalToggle,
  showModal,
  character,
  skillModifier,
  setCharacter,
}) => {
  const { darkMode } = useContext(ThemeContext);
  const [levelingUpTo, setLevelingUpTo] = useState(0);
  const [newHp, setNewHp] = useState(0);
  const [firstAttribute, setFirstAttribute] = useState("");
  const [secondAttribute, setSecondAttribute] = useState("");

  useEffect(() => {
    const classHitDie = character.class?.hitDie + 1;
    if (character.rollForHp) {
      const hpGain =
        Math.floor(Math.random() * (classHitDie - 1) + 1) +
        character.constitutionModifier;
      setNewHp(character.hitPoints + hpGain);
    } else {
      const hpGain =
        character.class?.hitDie / 2 + character.constitutionModifier;
      setNewHp(character.hitPoints + hpGain);
    }

    setLevelingUpTo(character.level + 1);
  }, [character.level, character.hitPoints, character.constitutionModifier]);

  const attributes = [
    "Strength",
    "Dexterity",
    "Constitution",
    "Wisdom",
    "Intelligence",
    "Charisma",
  ];

  const handleLevelUp = () => {
    const updatedCharacter = { ...character };

    if (firstAttribute !== "") {
      const attributeOne = firstAttribute.toLowerCase();
      updatedCharacter[attributeOne] += 1;
    }
    if (secondAttribute !== "") {
      const attributeTwo = secondAttribute.toLowerCase();
      updatedCharacter[attributeTwo] += 1;
    }

    const characterObj = {
      id: character.id,
      strength: updatedCharacter.strength,
      dexterity: updatedCharacter.dexterity,
      constitution: updatedCharacter.constitution,
      wisdom: updatedCharacter.wisdom,
      intelligence: updatedCharacter.intelligence,
      charisma: updatedCharacter.charisma,
      subClassId: parseInt(character.subClassId),
      level: levelingUpTo,
      hitPoints: newHp,
    };

    levelUpCharacter(characterObj).then(() => {
      getCharacterById(character.id).then(setCharacter);
    });
    modalToggle();
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;

    setCharacter((prev) => ({
      ...prev,
      [name]: value,
    }));
  };
  return (
    <Modal
      show={showModal}
      onHide={modalToggle}
      size="xl"
      data-bs-theme={darkMode ? "dark" : "light"}
    >
      <Modal.Header closeButton className="text-center">
        <h5>
          <Row>
            <Col>{character.level}</Col>{" "}
            <Col>{<FontAwesomeIcon icon="fa-solid fa-arrow-right" />} </Col>
            <Col>{levelingUpTo}</Col>
          </Row>
        </h5>
      </Modal.Header>
      <Modal.Body>
        <Row className="text-center">
          <Col>
            <small>Old HP</small>
            <p>{character.hitPoints}</p>
          </Col>
          <Col>
            <small>New HP</small>
            <p>{newHp}</p>
          </Col>
        </Row>
        {levelingUpTo === 3 ? (
          <Form>
            <Form.Label htmlFor="characterLevelUp-subClass">
              SubClass
            </Form.Label>
            <Form.Select
              id="characterLevelUp-subclass"
              name="subClassId"
              onChange={(e) => handleInputChange(e)}
            >
              <option value={0}>Choose Subclass</option>
              {character.class?.subClasses?.map((sc) => (
                <option key={sc.id} value={sc.id}>
                  {sc.name}
                </option>
              ))}
            </Form.Select>
          </Form>
        ) : levelingUpTo % 4 === 0 ? (
          <Container className="text-center">
            <Row className="mb-4">
              <Col className="characterSheet-traits">
                <small>Str</small>
                <p>{skillModifier(character.strengthModifier)}</p>
                <p>{character.strength}</p>
              </Col>
              <Col className="characterSheet-traits">
                <small>Dex</small>
                <p>{skillModifier(character.dexterityModifier)}</p>
                <p>{character.dexterity}</p>
              </Col>
              <Col className="characterSheet-traits">
                <small>Con</small>
                <p>{skillModifier(character.constitutionModifier)}</p>
                <p>{character.constitution}</p>
              </Col>
              <Col className="characterSheet-traits">
                <small>Wis</small>
                <p>{skillModifier(character.wisdomModifier)}</p>
                <p>{character.wisdom}</p>
              </Col>
              <Col className="characterSheet-traits">
                <small>Int</small>
                <p>{skillModifier(character.intelligenceModifier)}</p>
                <p>{character.intelligence}</p>
              </Col>
              <Col className="characterSheet-traits">
                <small>Cha</small>
                <p>{skillModifier(character.charismaModifier)}</p>
                <p>{character.charisma}</p>
              </Col>
            </Row>
            <h4 className="mb-4">
              Raise 2 attributes by 1 or 1 attribute by 2
            </h4>
            <Form>
              <Form.Group className="mb-4">
                <Form.Label htmlFor="characterLevelUp-attributeSelectOne">
                  Attribute 1
                </Form.Label>
                <Form.Select
                  id="characterLevelUp-attributeSelectOne"
                  name="attributeOne"
                  onChange={(e) => setFirstAttribute(e.target.value)}
                >
                  <option value={""}>Select Attribute</option>
                  {attributes.map((a) => (
                    <option key={a} value={a}>
                      {a}
                    </option>
                  ))}
                </Form.Select>
              </Form.Group>
              <Form.Group>
                <Form.Label htmlFor="characterLevelUp-attributeSelectTwo">
                  Attribute 2
                </Form.Label>
                <Form.Select
                  id="characterLevelUp-attributeSelectTwo"
                  name="attributeTwo"
                  onChange={(e) => setSecondAttribute(e.target.value)}
                >
                  <option value={""}>Select Attribute</option>
                  {attributes.map((a) => (
                    <option key={a} value={a}>
                      {a}
                    </option>
                  ))}
                </Form.Select>
              </Form.Group>
            </Form>
          </Container>
        ) : (
          ""
        )}
      </Modal.Body>
      <Modal.Footer>
        <Button
          className="btn-primary"
          onClick={() => handleLevelUp(firstAttribute, secondAttribute)}
        >
          Save
        </Button>{" "}
        <Button className="btn-primary" onClick={modalToggle}>
          Cancel
        </Button>
      </Modal.Footer>
    </Modal>
  );
};
