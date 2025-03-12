import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Button, Col, Form, Row } from "react-bootstrap";
import { ThemeContext } from "../../../ThemeContext/ThemeContext";
import { useContext } from "react";
import { AbilityScoreRoller } from "./AbilityScoreRoller";

export const CreateStepThree = ({
  character,
  handleInputChange,
  setStep,
  setObjectLength,
  buttonIsDisabled,
}) => {
  const { darkMode, setDarkMode } = useContext(ThemeContext);
  return (
    <Form className="mt-5 " data-bs-theme={darkMode ? "dark" : "light"}>
      <h2>Enter ability scores</h2>
      <Form.Group className="mb-5 ">
        <Row>
          <Col>
            <small className="primaryText-color-bold">Str</small>
            <Form.Control
              className="newCharacter-abilityScore primaryText-color"
              type="number"
              placeholder="10"
              name="strength"
              value={character.strength}
              required
              onChange={(e) => handleInputChange(e)}
            />
          </Col>
          <Col>
            <small className="primaryText-color-bold">Dex</small>
            <Form.Control
              className="newCharacter-abilityScore primaryText-color"
              type="number"
              placeholder="10"
              name="dexterity"
              value={character.dexterity}
              required
              onChange={(e) => handleInputChange(e)}
            />
          </Col>
          <Col>
            <small className="primaryText-color-bold">Con</small>
            <Form.Control
              className="newCharacter-abilityScore primaryText-color"
              type="number"
              placeholder="10"
              name="constitution"
              value={character.constitution}
              required
              onChange={(e) => handleInputChange(e)}
            />
          </Col>
          <Col>
            <small className="primaryText-color-bold">Wis</small>
            <Form.Control
              className="newCharacter-abilityScore primaryText-color"
              type="number"
              name="wisdom"
              placeholder="10"
              value={character.wisdom}
              required
              onChange={(e) => handleInputChange(e)}
            />
          </Col>
          <Col>
            <small className="primaryText-color-bold">Int</small>
            <Form.Control
              className="newCharacter-abilityScore primaryText-color"
              type="number"
              name="intelligence"
              placeholder="10"
              value={character.intelligence}
              required
              onChange={(e) => handleInputChange(e)}
            />
          </Col>
          <Col>
            <small className="primaryText-color-bold">Cha</small>
            <Form.Control
              className="newCharacter-abilityScore primaryText-color"
              type="number"
              name="charisma"
              placeholder="10"
              value={character.charisma}
              required
              onChange={(e) => handleInputChange(e)}
            />
          </Col>
        </Row>
      </Form.Group>
      <AbilityScoreRoller />
      <Row>
        <Col>
          <Button
            className="btn-primary"
            disabled={buttonIsDisabled}
            onClick={() => {
              setStep(2);
              setObjectLength(10);
            }}
          >
            <FontAwesomeIcon icon="fa-solid fa-arrow-left" />
          </Button>
        </Col>
        <Col>
          <Button
            className="btn-primary"
            disabled={buttonIsDisabled}
            onClick={() => {
              setStep(4);
              setObjectLength(19);
            }}
          >
            <FontAwesomeIcon icon="fa-solid fa-arrow-right" />
          </Button>
        </Col>
      </Row>
    </Form>
  );
};
