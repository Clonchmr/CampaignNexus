import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Button, Col, Form, Row } from "react-bootstrap";
import { ThemeContext } from "../../../ThemeContext/ThemeContext";
import { useContext } from "react";

export const CreateStepOne = ({
  handleInputChange,
  character,
  buttonIsDisabled,
  setObjectLength,
  setStep,
}) => {
  const { darkMode, setDarkMode } = useContext(ThemeContext);
  return (
    <Form data-bs-theme={darkMode ? "dark" : "light"} className="mt-5">
      <Form.Group className="mb-4">
        <Form.Label htmlFor="newCharacter-name">Character Name</Form.Label>
        <Form.Control
          id="newCharacter-name"
          className="lowerCaseFont"
          type="text"
          name="name"
          value={character.name}
          required
          placeholder="Name"
          onChange={(e) => handleInputChange(e)}
        />
      </Form.Group>
      <Form.Group className="mb-4">
        <Row>
          <Form.Label htmlFor="newCharacter-heightFeet">Height</Form.Label>
          <Col>
            <Form.Control
              id="newCharacter-heightFeet"
              className="lowerCaseFont"
              type="number"
              name="feet"
              value={character.feet}
              required
              placeholder="ft."
              onChange={(e) => handleInputChange(e)}
            />
          </Col>
          <Col>
            <Form.Control
              id="newCharacter-heightInches"
              className="lowerCaseFont"
              type="number"
              name="inches"
              value={character.inches}
              required
              placeholder="in."
              onChange={(e) => handleInputChange(e)}
            />
          </Col>
        </Row>
      </Form.Group>
      <Form.Group className="mb-4">
        <Form.Label htmlFor="newCharacter-weight">Weight</Form.Label>
        <Form.Control
          id="newCharacter-weight"
          className="lowerCaseFont"
          type="number"
          name="weight"
          value={character.weight}
          required
          placeholder="lbs."
          onChange={(e) => handleInputChange(e)}
        />
      </Form.Group>
      <Form.Group>
        <Form.Label htmlFor="newCharacter-age">Age</Form.Label>
        <Form.Control
          id="newCharacter-age"
          className="lowerCaseFont"
          type="number"
          name="age"
          value={character.age}
          required
          placeholder="Age"
          onChange={(e) => handleInputChange(e)}
        />
      </Form.Group>
      <Form.Group className="mb-4">
        <Form.Label htmlFor="newCharacter-gender">Gender</Form.Label>
        <Form.Control
          id="newCharacter-gender"
          className="lowerCaseFont"
          type="text"
          name="gender"
          value={character.gender}
          required
          placeholder="Gender"
          onChange={(e) => handleInputChange(e)}
        />
      </Form.Group>
      <Form.Group>
        <Button
          className="btn-primary"
          disabled={buttonIsDisabled}
          onClick={() => {
            setStep(2);
            setObjectLength(10);
          }}
        >
          <FontAwesomeIcon icon="fa-solid fa-arrow-right" />
        </Button>
      </Form.Group>
    </Form>
  );
};
