import { useContext, useEffect, useState } from "react";
import { Button, Col, Container, Form, Row } from "react-bootstrap";
import { getAllItems } from "../../../managers/itemManager";
import { ThemeContext } from "../../../ThemeContext/ThemeContext";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

export const CreateStepFour = ({
  character,
  setStep,
  setObjectLength,
  handleInputChange,
  buttonIsDisabled,
}) => {
  const [items, setItems] = useState([]);
  const { darkMode } = useContext(ThemeContext);

  useEffect(() => {
    getAllItems().then(setItems);
  }, []);
  return (
    <Container className="mt-5">
      <h2>Choose Starting Equipment</h2>
      <Form data-bs-theme={darkMode ? "dark" : "light"}>
        <Form.Group className="mb-4">
          <Form.Label htmlFor="createCharacter-armor">
            Select starting armor
          </Form.Label>
          <Form.Select
            id="createCharacter-armor"
            className="lowerCaseFont"
            name="armorItem"
            onChange={(e) => handleInputChange(e)}
          >
            <option value={0}>Choose armor</option>
            {items
              .filter((i) => i.itemType === "Armor")
              .map((i) => (
                <option key={i.id} value={i.id}>
                  {i.itemName}
                </option>
              ))}
          </Form.Select>
        </Form.Group>
        <Form.Group className="mb-4">
          <Form.Label htmlFor="createCharacter-weapon">
            Select starting weapon
          </Form.Label>
          <Form.Select
            id="createCharacter-weapon"
            className="lowerCaseFont"
            name="weaponItem"
            onChange={(e) => handleInputChange(e)}
          >
            <option value={0}>Choose weapon</option>
            {items
              .filter((i) => i.itemType === "Weapon")
              .map((i) => (
                <option key={i.id} value={i.id}>
                  {i.itemName}
                </option>
              ))}
          </Form.Select>
        </Form.Group>
        <Form.Group className="mb-4">
          <Form.Label htmlFor="createCharacter-miscItem">
            Select misc item
          </Form.Label>
          <Form.Select
            id="createCharacter-miscItem"
            className="lowerCaseFont"
            name="miscItem"
            onChange={(e) => handleInputChange(e)}
          >
            <option value={0}>Choose whatever you want</option>
            {items.map((i) => (
              <option key={i.id} value={i.id}>
                {i.itemName}
              </option>
            ))}
          </Form.Select>
        </Form.Group>
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
                setStep(5);
                setObjectLength(19);
              }}
            >
              <FontAwesomeIcon icon="fa-solid fa-arrow-right" />
            </Button>
          </Col>
        </Row>
      </Form>
    </Container>
  );
};
