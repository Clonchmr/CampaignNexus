import { useContext, useEffect, useState } from "react";
import { Button, Form, Offcanvas, Table } from "react-bootstrap";
import { ThemeContext } from "../../ThemeContext/ThemeContext";
import { getAllItems, newCharacterItem } from "../../managers/itemManager";
import { getCharacterById } from "../../managers/characterManager";

export const AddItemOffcanvas = ({
  itemOffCanvas,
  itemOffCanvasToggle,
  character,
  setCharacter,
}) => {
  const { darkMode, setDarkMode } = useContext(ThemeContext);
  const [items, setItems] = useState([]);
  const [chosenItem, setChosenItem] = useState({ id: 0, quantity: 1 });

  useEffect(() => {
    getAllItems().then(setItems);
  }, []);

  const handleAddItem = (itemId) => {
    let quantity = 0;
    if (itemId != chosenItem.id) {
      quantity = 1;
    } else {
      quantity = chosenItem.quantity;
    }

    newCharacterItem(itemId, character.id, quantity)
      .then(() => {
        getCharacterById(character.id).then(setCharacter);
      })
      .then(itemOffCanvasToggle());
  };
  return (
    <Offcanvas
      show={itemOffCanvas}
      onHide={itemOffCanvasToggle}
      placement="end"
      data-bs-theme={darkMode ? "dark" : "light"}
      style={{ width: "30%" }}
    >
      <Offcanvas.Header closeButton></Offcanvas.Header>
      <Offcanvas.Body>
        <Table hover>
          <thead>
            <tr>
              <th>Name</th>
              <th>Effect</th>
              <th>Weight</th>
              <th>Qty</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            {items.map((i) => (
              <tr key={i.id}>
                <td>{i.itemName}</td>
                <td>
                  {i.damage ? (
                    <small>Dmg: {i.damage}</small>
                  ) : i.armorClass ? (
                    <small>Ac: {i.armorClass}</small>
                  ) : i.notes && i.itemType === "Consumable" ? (
                    <small>Healing: {i.notes}</small>
                  ) : (
                    <small>-</small>
                  )}
                </td>
                <td>{i.weight} lbs.</td>
                <td>
                  <Form>
                    <Form.Group>
                      <Form.Control
                        type="number"
                        min={1}
                        defaultValue={1}
                        onChange={(e) => {
                          const data = { ...chosenItem };
                          data.id = i.id;
                          data.quantity = e.target.value;
                          setChosenItem(data);
                        }}
                      />
                    </Form.Group>
                  </Form>
                </td>
                <td>
                  <Button
                    className="btn-primary"
                    onClick={() => handleAddItem(i.id)}
                  >
                    Add
                  </Button>
                </td>
              </tr>
            ))}
          </tbody>
        </Table>
      </Offcanvas.Body>
    </Offcanvas>
  );
};
