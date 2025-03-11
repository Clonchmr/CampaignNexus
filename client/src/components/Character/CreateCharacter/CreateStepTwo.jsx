import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Alert, Button, Col, Form, Row } from "react-bootstrap";
import { ThemeContext } from "../../../ThemeContext/ThemeContext";
import { useContext, useEffect, useState } from "react";

export const CreateStepTwo = ({
  character,
  handleClassChoice,
  handleInputChange,
  classes,
  chosenClass,
  buttonIsDisabled,
  setStep,
  setObjectLength,
  species,
}) => {
  const { darkMode, setDarkMode } = useContext(ThemeContext);
  const [foundSpecies, setFoundSpecies] = useState({});

  useEffect(() => {
    const chosenSpecies = species.find((s) => s.id == character.speciesId);
    setFoundSpecies(chosenSpecies);
  }, [character.speciesId, species]);
  return (
    <Form data-bs-theme={darkMode ? "dark" : "light"} className="mt-5">
      <Form.Group className="mb-4">
        <Form.Label htmlFor="createCharacter-species">Species</Form.Label>
        <Form.Select
          id="createCharacter-species"
          className="lowerCaseFont"
          name="speciesId"
          value={character.speciesId}
          onChange={(e) => {
            handleInputChange(e);
          }}
        >
          <option value={0}>Choose species</option>
          {species.map((s) => (
            <option key={s.id} value={s.id}>
              {s.speciesName}
            </option>
          ))}
        </Form.Select>
        {character.speciesId && character.speciesId != 0 && (
          <Alert
            variant={darkMode ? "dark" : "light"}
            className="lowerCaseFont"
          >
            <p className="alertText">Speed: {foundSpecies?.speed} ft.</p>
            <p className="alertText">{foundSpecies?.description}</p>
          </Alert>
        )}
      </Form.Group>
      <Form.Group className="mb-4">
        <Form.Label htmlFor="newCharacter-class">Class</Form.Label>
        <Form.Select
          className="lowerCaseFont"
          name="classId"
          value={character.classId}
          onChange={(e) => handleClassChoice(e)}
        >
          <option value="">Choose your class</option>
          {classes.map((c) => (
            <option key={c.id} value={c.id}>
              {c.className}
            </option>
          ))}
        </Form.Select>
      </Form.Group>
      {character.classId && character.classId !== "" && (
        <>
          <Form.Group className="mb-4">
            <Form.Label htmlFor="newCharacter-abilityOne">
              Choose First Ability
            </Form.Label>
            <Form.Select
              id="newCharacter-abilityOne"
              className="lowerCaseFont"
              name="abilityOneId"
              value={character.abilityOneId}
              required
              onChange={(e) => handleInputChange(e)}
            >
              <option value="">Choose first ability</option>
              {chosenClass.classAbilities?.map((ca) => (
                <option key={ca.ability?.id} value={ca.ability?.id}>
                  {ca.ability?.abilityName}
                </option>
              ))}
            </Form.Select>
          </Form.Group>
          <Form.Group className="mb-5">
            <Form.Label htmlFor="newCharacter-abilityTwo">
              Choose Second Ability
            </Form.Label>
            <Form.Select
              id="newCharacter-abilityTwo"
              className="lowerCaseFont"
              name="abilityTwoId"
              value={character.abilityTwoId}
              required
              onChange={(e) => handleInputChange(e)}
            >
              <option value="">Choose second ability</option>
              {chosenClass.classAbilities?.map((ca) => (
                <option key={ca.ability?.id} value={ca.ability?.id}>
                  {ca.ability?.abilityName}
                </option>
              ))}
            </Form.Select>
          </Form.Group>
        </>
      )}
      <Row>
        <Col>
          <Button
            className="btn-primary"
            disabled={buttonIsDisabled}
            onClick={() => {
              setStep(1);
              setObjectLength(6);
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
              setStep(3);
              setObjectLength(16);
            }}
          >
            <FontAwesomeIcon icon="fa-solid fa-arrow-right" />
          </Button>
        </Col>
      </Row>
    </Form>
  );
};
