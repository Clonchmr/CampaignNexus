import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getCharacterById } from "../../managers/characterManager";
import {
  Button,
  Col,
  Container,
  Image,
  Row,
  Toast,
  ToastContainer,
} from "react-bootstrap";
import "../../styles/character.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

export const CharacterSheet = ({ darkMode, loggedInUser }) => {
  const [character, setCharacter] = useState({});
  const [shakingIcon, setShakingIcon] = useState({});
  const [toastTarget, setToastTarget] = useState({});
  const [showToast, setShowToast] = useState(false);

  const { characterId } = useParams();

  const toastToggle = () => setShowToast(!showToast);

  useEffect(() => {
    getCharacterById(characterId).then(setCharacter);
  }, [characterId]);

  const handleSkillRoll = (i, skillName, modifier) => {
    const roll = Math.floor(Math.random() * (0, 21)) + modifier;
    setToastTarget({ name: skillName, modifier: modifier, roll: roll });
    setShakingIcon((prev) => ({ ...prev, [i]: true }));

    setTimeout(() => {
      setShakingIcon((prev) => ({ ...prev, [i]: false }));
    }, 500);

    toastToggle();
  };

  const skillsArray = [
    { name: "Acrobatics", modifier: character.dexterityModifier },
    { name: "Animal Handling", modifier: character.wisdomModifier },
    { name: "Arcana", modifier: character.intelligenceModifier },
    { name: "Athletics", modifier: character.intelligenceModifier },
    { name: "Deception", modifier: character.charismaModifier },
    { name: "History", modifier: character.intelligenceModifier },
    { name: "Insight", modifier: character.wisdomModifier },
    { name: "Intimidation", modifier: character.charismaModifier },
    { name: "Medicine", modifier: character.wisdomModifier },
    { name: "Nature", modifier: character.intelligenceModifier },
    { name: "Perception", modifier: character.wisdomModifier },
    { name: "Performance", modifier: character.charismaModifier },
    { name: "Persuasion", modifier: character.charismaModifier },
    { name: "Religion", modifier: character.wisdomModifier },
    { name: "Sleight of Hand", modifier: character.dexterityModifier },
    { name: "Stealth", modifier: character.dexterityModifier },
    { name: "Survival", modifier: character.wisdomModifier },
  ];
  const skillModifier = (modifier) => {
    if (modifier >= 0) {
      return ` + ${modifier}`;
    } else {
      return ` ${modifier}`;
    }
  };

  return (
    <Container>
      <Container
        className="characterSheet-container campaignDetails-container mt-5"
        style={{ width: "80rem" }}
      >
        <Row className="characterSheet-header mb-5">
          <Col>
            <Button className="btn-primary">Level Up!</Button>
          </Col>
          <Col>
            <Button className="btn-primary">Edit Character</Button>
          </Col>
          <Col className="characterSheet-traits">
            <span>
              <h6>Initiative</h6>
              {skillModifier(character.dexterityModifier)}
            </span>
          </Col>

          <Col className="characterSheet-traits">
            <span>
              <h6>AC</h6>
              {10}
            </span>
          </Col>
          <Col className="characterSheet-traits">
            <span>
              <h6>Speed</h6>
              {character.species?.speed}
            </span>
          </Col>
          <Col className="characterSheet-traits">
            <span>
              <h6>HP</h6>
              {character.hitPoints}
            </span>
          </Col>
        </Row>
        <Row>
          <Col>
            <Image
              alt={`Image for character ${character.name}`}
              src={character.characterPicUrl}
              style={{ maxWidth: "15rem" }}
            />
          </Col>
          <Col>
            <h4>{character.name}</h4>
            <p>Level: {character.level}</p>
            <p>{character.species?.speciesName}</p>
            {character.subClass && <p>{character.subClass?.name}</p>}
            <p>{character.class?.className}</p>
          </Col>
          <Col className="characterSheet-traits">
            <h6>STR</h6>
            <Button
              className="characterSheet-abilityScore-btn"
              onClick={() =>
                handleSkillRoll(0, "Strength", character.strengthModifier)
              }
            >
              {" "}
              {skillModifier(character.strengthModifier)}
            </Button>
            <p className="characterSheet-abilityScore">{character.strength}</p>
          </Col>
          <Col className="characterSheet-traits">
            <h6>DEX</h6>
            <Button
              className="characterSheet-abilityScore-btn"
              onClick={() =>
                handleSkillRoll(0, "Dexterity", character.dexterityModifier)
              }
            >
              {" "}
              {skillModifier(character.dexterityModifier)}{" "}
            </Button>
            <p className="characterSheet-abilityScore">{character.dexterity}</p>
          </Col>
          <Col className="characterSheet-traits">
            <h6>CON</h6>
            <Button
              className="characterSheet-abilityScore-btn"
              onClick={() =>
                handleSkillRoll(
                  0,
                  "Constitution",
                  character.constitutionModifier
                )
              }
            >
              {" "}
              {skillModifier(character.constitutionModifier)}{" "}
            </Button>
            <p className="characterSheet-abilityScore">
              {character.constitution}
            </p>
          </Col>
          <Col className="characterSheet-traits">
            <h6>WIS</h6>
            <Button
              className="characterSheet-abilityScore-btn"
              onClick={() =>
                handleSkillRoll(0, "Wisdom", character.wisdomModifier)
              }
            >
              {" "}
              {skillModifier(character.wisdomModifier)}{" "}
            </Button>
            <p className="characterSheet-abilityScore">{character.wisdom}</p>
          </Col>
          <Col className="characterSheet-traits">
            <h6>INT</h6>
            <Button
              className="characterSheet-abilityScore-btn"
              onClick={() =>
                handleSkillRoll(
                  0,
                  "Intelligence",
                  character.intelligenceModifier
                )
              }
            >
              {skillModifier(character.intelligenceModifier)}{" "}
            </Button>
            <p className="characterSheet-abilityScore">
              {character.intelligence}
            </p>
          </Col>
          <Col className="characterSheet-traits">
            <h6>CHA</h6>
            <Button
              className="characterSheet-abilityScore-btn"
              onClick={() =>
                handleSkillRoll(0, "Charisma", character.charismaModifier)
              }
            >
              {skillModifier(character.charismaModifier)}{" "}
            </Button>
            <p className="characterSheet-abilityScore">{character.charisma}</p>
          </Col>
        </Row>
        <Row>
          <Col className="characterSheet-skills-container mt-5">
            {skillsArray.map((s, i) => (
              <p key={i}>
                {`${s.name} ${skillModifier(s.modifier)}`}{" "}
                <FontAwesomeIcon
                  icon="fa-solid fa-dice-d20"
                  className={`${shakingIcon[i] ? "fa-shake" : ""} diceIcon`}
                  style={{ color: "#6e0d25" }}
                  onClick={() => handleSkillRoll(i, s.name, s.modifier)}
                />
              </p>
            ))}
          </Col>
          <Col className="characterSheet-savingThrowsColumn">
            <Row>
              <h4>Saving Throws</h4>
              <Col>
                <p>{`Str ${skillModifier(character.strengthModifier)}`} </p>
                <p>{`Dex ${skillModifier(character.dexterityModifier)}`}</p>
              </Col>
              <Col>
                <p>{`Con ${skillModifier(character.constitutionModifier)}`}</p>
                <p>{`Int ${skillModifier(character.intelligenceModifier)}`}</p>
              </Col>
              <Col>
                <p>{`Wis ${skillModifier(character.wisdomModifier)}`}</p>
                <p>{`Cha ${skillModifier(character.charismaModifier)}`}</p>
              </Col>
            </Row>
            <div>Navbar thingy</div>
          </Col>
        </Row>
        <ToastContainer className="toastContainer p-5" position="bottom-end">
          <Toast
            onClose={toastToggle}
            show={showToast}
            delay={4000}
            autohide
            data-bs-theme={darkMode ? "dark" : "light"}
          >
            <Toast.Header
              className="d-flex justify-content-between w-100"
              closeButton={false}
            >
              <strong className="mx-auto">{toastTarget.name}</strong>
              <small>{skillModifier(toastTarget.modifier)}</small>
            </Toast.Header>
            <Toast.Body>{toastTarget.roll}</Toast.Body>
          </Toast>
        </ToastContainer>
        {loggedInUser.id === character.userId && (
          <Button className="mt-3">Delete Character</Button>
        )}
      </Container>
    </Container>
  );
};
