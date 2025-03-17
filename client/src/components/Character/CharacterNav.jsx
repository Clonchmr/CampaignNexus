import { useContext, useEffect, useState } from "react";
import {
  Button,
  Col,
  Container,
  Form,
  Row,
  Tab,
  Table,
  Tabs,
} from "react-bootstrap";
import "../../styles/character.css";
import { ThemeContext } from "../../ThemeContext/ThemeContext";
import { toggleEquipItem } from "../../managers/itemManager";
import { getCharacterById } from "../../managers/characterManager";
import { SpellOffcanvas } from "../OffCanvas/SpellOffcanvas";
import { getAllAlignments } from "../../managers/alignmentManager";
import { AddItemOffcanvas } from "../OffCanvas/AddItemOffcanvas";
import { useConsumable } from "../../managers/characterItem";

export const CharacterNav = ({
  character,
  setCharacter,
  handleInputChange,
  editCharacter,
}) => {
  const [spellSaveDc, setSpellSaveDc] = useState(0);
  const [spellAttack, setSpellAttack] = useState(0);
  const [spellOffcanvas, setSpellOffcanvas] = useState(false);
  const [itemOffCanvas, setItemOffcanvas] = useState(false);
  const [spellTarget, setSpellTarget] = useState({});
  const [alignments, setAlignments] = useState([]);
  const { darkMode, setDarkMode } = useContext(ThemeContext);

  const spellOffCanvasToggle = () => setSpellOffcanvas(!spellOffcanvas);
  const itemOffCanvasToggle = () => setItemOffcanvas(!itemOffCanvas);

  useEffect(() => {
    const cClass = character?.class?.className;
    let spellcastingAbility = 0;

    if (
      cClass === "Sorcerer" ||
      cClass === "Warlock" ||
      cClass === "Bard" ||
      cClass === "Paladin"
    ) {
      spellcastingAbility = character?.charismaModifier;
    } else if (
      cClass === "Ranger" ||
      cClass === "Monk" ||
      cClass === "Druid" ||
      cClass === "Cleric"
    ) {
      spellcastingAbility = character?.wisdomModifier;
    } else {
      spellcastingAbility = character?.intelligenceModifier;
    }

    setSpellSaveDc(spellcastingAbility + character?.level + 8);
    setSpellAttack(spellcastingAbility + character?.level);
  }, [character]);

  useEffect(() => {
    getAllAlignments().then(setAlignments);

    if (character?.height) {
      const [feet, inches] = character.height
        .split("'")
        .map((num) => num.trim());
      setCharacter((prevCharacter) => ({
        ...prevCharacter,
        feet: feet || "0",
        inches: inches || "0",
      }));
    }
  }, []);

  const handleUseConsumable = (e, characterConsumableId) => {
    e.stopPropagation();
    useConsumable(characterConsumableId).then(() => {
      getCharacterById(character.id).then(setCharacter);
    });
  };
  return (
    <Tabs defaultActiveKey="Actions" id="characterSheet-nav" fill>
      <Tab eventKey="Actions" title="Actions">
        <Table hover className="mt-4 characterSheet-navTable">
          <thead>
            <tr>
              <th>Action</th>
              <th>Range</th>
              <th>Damage</th>
              <th>Notes</th>
            </tr>
          </thead>
          <tbody>
            {character?.characterItems
              ?.filter((ci) => ci.item?.itemType === "Weapon" && ci.isEquipped)
              .map((i, index) => (
                <tr key={index}>
                  <td className="characterSheet-navTable">
                    {i.item?.itemName}
                  </td>
                  <td className="characterSheet-navTable">{i.item?.range}</td>
                  <td>{i.item?.damage}</td>
                  <td>{i.item?.notes}</td>
                </tr>
              ))}
            {character?.characterAbilities
              ?.filter((ca) => ca.ability?.abilityType.includes("Action"))
              .map((a) => (
                <tr
                  key={a.ability?.id}
                  style={{ cursor: "pointer" }}
                  onClick={() => {
                    setSpellTarget(a);
                    spellOffCanvasToggle();
                  }}
                >
                  <td className="characterSheet-navTable">
                    {a.ability?.abilityName}
                  </td>
                  <td>{a.ability?.range}</td>
                  {a.ability?.diceNumber ? (
                    <td>{`${a.ability?.numberOfDice} D${a.ability?.diceNumber}`}</td>
                  ) : (
                    <td></td>
                  )}
                  <td>{a.ability?.notes}</td>
                </tr>
              ))}
          </tbody>
        </Table>
      </Tab>
      <Tab eventKey="Spells" title="Spells">
        <Row className="mt-3 mb-3">
          <Col>
            <h6>Save DC {spellSaveDc}</h6>
          </Col>
          <Col>
            <h6>Spell Attack {spellAttack}</h6>
          </Col>
        </Row>
        <Table hover className="characterSheet-navTable">
          <thead>
            <tr>
              <th>Name</th>
              <th>Time</th>
              <th>Range</th>
              <th>Hit/DC</th>
              <th>Effect</th>
              <th>Notes</th>
            </tr>
          </thead>
          <tbody>
            {character?.characterAbilities
              ?.filter((ca) => !ca.ability?.abilityType?.includes("Action"))
              .map((a) => (
                <tr
                  key={a.ability?.id}
                  style={{ cursor: "pointer" }}
                  onClick={() => {
                    setSpellTarget(a);
                    spellOffCanvasToggle();
                  }}
                >
                  <td>{a.ability?.abilityName}</td>
                  <td>{a.ability?.castingTime}</td>
                  <td>{a.ability?.range}</td>
                  <td>
                    {a.ability?.savingThrow
                      ? `${spellSaveDc} ${a.ability?.savingThrow}`
                      : `+ ${spellAttack}`}
                  </td>
                  <td>
                    {a.ability?.numberOfDice
                      ? `${a.ability?.numberOfDice} d${a.ability?.diceNumber}`
                      : ""}
                  </td>
                  <td>{a.ability?.notes}</td>
                </tr>
              ))}
          </tbody>
        </Table>
        <SpellOffcanvas
          spellTarget={spellTarget}
          spellOffCanvasToggle={spellOffCanvasToggle}
          spellOffcanvas={spellOffcanvas}
        />
      </Tab>
      <Tab eventKey="Inventory" title="Inventory">
        <p className="mt-4">{`Weight Carried: ${character?.totalWeight} lbs.`}</p>
        <Table hover className="characterSheet-navTable">
          <thead>
            <tr>
              <th>Name</th>
              <th>Weight</th>
              <th>Qty</th>
              <th>Note</th>
              <th>
                <Button
                  className="btn-primary"
                  style={{ height: "2rem", fontSize: "0.8rem" }}
                  onClick={itemOffCanvasToggle}
                >
                  Add Item
                </Button>
              </th>
            </tr>
          </thead>
          <tbody>
            {character?.characterItems?.map((i) => (
              <tr
                key={i.item?.id}
                onClick={() => {
                  setSpellTarget(i);
                  spellOffCanvasToggle();
                }}
                style={{ cursor: "pointer" }}
              >
                <td>{i.item?.itemName}</td>
                <td>{`${i.item?.weight} lbs.`}</td>
                <td>{i.quantity}</td>
                <td>{i.item?.notes}</td>
                <td>
                  {i.item?.itemType !== "Consumable" ? (
                    <Form>
                      <Form.Group>
                        <Form.Label>Equipped</Form.Label>
                        <Form.Check
                          checked={i.isEquipped}
                          onClick={(e) => e.stopPropagation()}
                          onChange={() => {
                            {
                              toggleEquipItem(i.id).then(() => {
                                getCharacterById(character?.id).then(
                                  setCharacter
                                );
                              });
                            }
                          }}
                        />
                      </Form.Group>
                    </Form>
                  ) : (
                    <Button onClick={(e) => handleUseConsumable(e, i.id)}>
                      Use
                    </Button>
                  )}
                </td>
              </tr>
            ))}
          </tbody>
        </Table>
        <AddItemOffcanvas
          itemOffCanvasToggle={itemOffCanvasToggle}
          itemOffCanvas={itemOffCanvas}
          character={character}
          setCharacter={setCharacter}
        />
      </Tab>
      <Tab eventKey="Info" title="Info">
        <h6 className="mt-4 mb-2">Characteristics</h6>
        <Row>
          <Col>
            {!editCharacter ? (
              <>
                <small className="lowerCaseFont">Alignment</small>
                <p>{character?.alignment?.name}</p>
              </>
            ) : (
              <>
                <small className="lowerCaseFont">Alignment</small>
                <Form.Select
                  className="mb-3"
                  data-bs-theme={darkMode ? "dark" : "light"}
                  value={character?.alignmentId}
                  name="alignmentId"
                  onChange={(e) => handleInputChange(e)}
                >
                  {alignments.map((a) => (
                    <option key={a.id} value={a.id}>
                      {a.name}
                    </option>
                  ))}
                </Form.Select>
              </>
            )}
            {!editCharacter ? (
              <>
                <small className="lowerCaseFont">Gender</small>
                <p>{character?.gender}</p>
              </>
            ) : (
              <>
                <small className="lowerCaseFont">Gender</small>
                <Form.Control
                  type="text"
                  name="gender"
                  value={character?.gender}
                  data-bs-theme={darkMode ? "dark" : "light"}
                  onChange={(e) => handleInputChange(e)}
                />
              </>
            )}
          </Col>
          <Col>
            {!editCharacter ? (
              <>
                <small className="lowerCaseFont">Height</small>
                <p>{character?.height}</p>
              </>
            ) : (
              <Form.Group className="mb-3">
                <Row>
                  <Col>
                    <small className="lowerCaseFont">Feet</small>
                    <Form.Control
                      type="number"
                      name="feet"
                      data-bs-theme={darkMode ? "dark" : "light"}
                      value={character?.feet || ""}
                      onChange={(e) => handleInputChange(e)}
                    />
                  </Col>
                  <Col>
                    <small className="lowerCaseFont">Inches</small>
                    <Form.Control
                      type="number"
                      name="inches"
                      data-bs-theme={darkMode ? "dark" : "light"}
                      value={character?.inches || ""}
                      onChange={(e) => handleInputChange(e)}
                    />
                  </Col>
                </Row>
              </Form.Group>
            )}
            {!editCharacter ? (
              <>
                <small className="lowerCaseFont">Age</small>
                <p>{character?.age}</p>
              </>
            ) : (
              <>
                <small className="lowerCaseFont">Age</small>
                <Form.Control
                  type="number"
                  name="age"
                  className="mb-4"
                  value={character?.age}
                  data-bs-theme={darkMode ? "dark" : "light"}
                  onChange={(e) => handleInputChange(e)}
                />
              </>
            )}
          </Col>
          <Col>
            {!editCharacter ? (
              <>
                <small className="lowerCaseFont">Weight</small>
                <p>{character?.weight}</p>
              </>
            ) : (
              <>
                <small className="lowerCaseFont">Weight</small>
                <Form.Control
                  type="text"
                  className="mb-3"
                  name="weight"
                  value={character?.weight}
                  data-bs-theme={darkMode ? "dark" : "light"}
                  onChange={(e) => handleInputChange(e)}
                />
              </>
            )}
            {!editCharacter ? (
              <>
                <small className="lowerCaseFont">Faith</small>
                <p>{character?.faith}</p>
              </>
            ) : (
              <>
                <small className="lowerCaseFont">Faith</small>
                <Form.Control
                  type="text"
                  name="faith"
                  value={character?.faith}
                  data-bs-theme={darkMode ? "dark" : "light"}
                  onChange={(e) => handleInputChange(e)}
                />
              </>
            )}
          </Col>
        </Row>
        <Container>
          {!editCharacter ? (
            <>
              <h6 className="mt-3">Backstory</h6>
              <p>{character?.backstory}</p>
            </>
          ) : (
            <>
              <small className="lowerCaseFont">Backstory</small>
              <Form.Control
                as="textarea"
                name="backstory"
                className=" lowerCaseFont"
                value={character?.backstory}
                data-bs-theme={darkMode ? "dark" : "light"}
                onChange={(e) => handleInputChange(e)}
              />
            </>
          )}
        </Container>
      </Tab>
    </Tabs>
  );
};
