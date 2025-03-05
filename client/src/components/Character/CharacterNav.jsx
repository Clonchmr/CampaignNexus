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

export const CharacterNav = ({ character, setCharacter }) => {
  const [spellSaveDc, setSpellSaveDc] = useState(0);
  const [spellAttack, setSpellAttack] = useState(0);
  const { darkMode, setDarkMode } = useContext(ThemeContext);

  useEffect(() => {
    const cClass = character.class?.className;
    let spellcastingAbility = 0;

    if (
      cClass === "Sorcerer" ||
      cClass === "Warlock" ||
      cClass === "Bard" ||
      cClass === "Paladin"
    ) {
      spellcastingAbility = character.charismaModifier;
    } else if (
      cClass === "Ranger" ||
      cClass === "Monk" ||
      cClass === "Druid" ||
      cClass === "Cleric"
    ) {
      spellcastingAbility = character.wisdomModifier;
    } else {
      spellcastingAbility = character.intelligenceModifier;
    }

    setSpellSaveDc(spellcastingAbility + character.level + 8);
    setSpellAttack(spellcastingAbility + character.level);
  }, [character]);

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
            {character.characterItems
              ?.filter((ci) => ci.item?.itemType === "Weapon")
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
            {character.characterAbilities
              ?.filter((ca) => ca.ability?.abilityType.includes("Action"))
              .map((a) => (
                <tr key={a.ability?.id}>
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
            {/* <tr>
              <td>Unarmed Strike</td>
              <td>5 ft.</td>
              <td>{character.strengthModifier + 1}</td>
              <td></td>
            </tr> */}
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
            {character.characterAbilities
              ?.filter((ca) => !ca.ability?.abilityType.includes("Action"))
              .map((a) => (
                <tr key={a.ability?.id}>
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
      </Tab>
      <Tab eventKey="Inventory" title="Inventory">
        <p className="mt-4">{`Weight Carried: ${character.totalWeight} lbs.`}</p>
        <Table className="characterSheet-navTable">
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
                >
                  Add Item
                </Button>
              </th>
            </tr>
          </thead>
          <tbody>
            {character.characterItems?.map((i) => (
              <tr key={i.item?.id}>
                <td>{i.item?.itemName}</td>
                <td>{`${i.item?.weight} lbs.`}</td>
                <td>{i.quantity}</td>
                <td>{i.item?.notes}</td>
                <td>
                  <Form>
                    <Form.Group>
                      <Form.Label>Equipped</Form.Label>
                      <Form.Check
                        checked={i.isEquipped}
                        onChange={() => {
                          {
                            toggleEquipItem(i.id).then(() => {
                              getCharacterById(character.id).then(setCharacter);
                            });
                          }
                        }}
                      />
                    </Form.Group>
                  </Form>
                </td>
              </tr>
            ))}
          </tbody>
        </Table>
      </Tab>
      <Tab eventKey="Info" title="Info">
        <h6 className="mt-4 mb-2">Characteristics</h6>
        <Row>
          <Col>
            <small className="lowerCaseFont">Alignment</small>
            <p>{character.alignment?.name}</p>
            <small className="lowerCaseFont">Gender</small>
            <p>{character.gender}</p>
          </Col>
          <Col>
            <small className="lowerCaseFont">Height</small>
            <p>{character.height}</p>
            <small className="lowerCaseFont">Age</small>
            <p>{character.age}</p>
          </Col>
          <Col>
            <small className="lowerCaseFont">Weight</small>
            <p>{character.weight}</p>
            <small className="lowerCaseFont">Faith</small>
            <p>{character.faith}</p>
          </Col>
        </Row>
        <Container>
          <h6 className="mt-3">Backstory</h6>
          <p>{character.backstory}</p>
        </Container>
      </Tab>
    </Tabs>
  );
};
