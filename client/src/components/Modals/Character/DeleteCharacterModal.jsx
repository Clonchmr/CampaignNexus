import { useContext, useState } from "react";
import {
  Button,
  Col,
  Container,
  Form,
  Modal,
  ModalBody,
  Row,
} from "react-bootstrap";
import { ThemeContext } from "../../../ThemeContext/ThemeContext";
import { deleteCharacter } from "../../../managers/characterManager";
import { useNavigate } from "react-router-dom";

export const DeleteCharacterModal = ({
  character,
  deleteModal,
  deleteModalToggle,
}) => {
  const [showModal, setShowModal] = useState(false);
  const { darkMode } = useContext(ThemeContext);

  const navigate = useNavigate();

  const handleDeleteCharacter = () => {
    deleteCharacter(character.id).then(() => {
      deleteModalToggle();
      navigate("/characters");
    });
  };
  return (
    <Modal
      show={deleteModal}
      onHide={deleteModalToggle}
      data-bs-theme={darkMode ? "dark" : "light"}
    >
      <Modal.Header closeButton>{`Delete ${character.name}?`}</Modal.Header>
      <Modal.Body>
        <p>Are you sure you want to delete this character?</p>
        <p>This action cannot be reversed.</p>
      </Modal.Body>
      <Modal.Footer>
        <Button className="btn-primary" onClick={handleDeleteCharacter}>
          Delete
        </Button>
        <Button className="btn-primary" onClick={deleteModalToggle}>
          Cancel
        </Button>
      </Modal.Footer>
    </Modal>
  );
};
