import { useContext, useEffect, useState } from "react";
import {
  Alert,
  Button,
  Col,
  Container,
  Form,
  Image,
  Row,
} from "react-bootstrap";
import { ThemeContext } from "../../../ThemeContext/ThemeContext";
import { CloudinaryUploadWidget } from "../../CloudinaryUploadWidget";
import { deleteImage } from "../../../managers/cloudinaryManager";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

export const CreateStepFour = ({
  character,
  alignments,
  buttonIsDisabled,
  setStep,
  setObjectLength,
  handleInputChange,
  uploadedImage,
  setUploadedImage,
  handleCreateCharacter,
}) => {
  const { darkMode, setDarkMode } = useContext(ThemeContext);
  const [chosenAlignment, setChosenAlignment] = useState({});

  useEffect(() => {
    const characterAlignment = alignments.find(
      (a) => a.id == character.alignmentId
    );
    setChosenAlignment(characterAlignment);
  }, [character.alignmentId, alignments]);

  const handleRemoveImage = () => {
    if (!uploadedImage) return;

    //Extract public ID from url
    const publicId = uploadedImage.split("/").pop().split(".")[0];

    const publicIdObj = {
      publicId: publicId,
    };
    deleteImage(publicIdObj).then(() => setUploadedImage(null));
  };

  return (
    <Form data-bs-theme={darkMode ? "dark" : "light"} className="mt-5">
      <Form.Group className="mb-4">
        <Form.Label htmlFor="createCharacter-alignment">Alignment</Form.Label>
        <Form.Select
          id="createCharacter-alignment"
          className="lowerCaseFont"
          required
          value={character.alignmentId}
          name="alignmentId"
          onChange={(e) => handleInputChange(e)}
        >
          <option value={0}>Choose alignment</option>
          {alignments.map((a) => (
            <option key={a.id} value={a.id}>
              {a.name}
            </option>
          ))}
        </Form.Select>
        {character.alignmentId && character.alignmentId != 0 && (
          <Alert variant={darkMode ? "dark" : "light"}>
            <p className="alertText">{chosenAlignment?.description}</p>
          </Alert>
        )}
      </Form.Group>
      <Form.Group>
        <Form.Label htmlFor="createCharacter-faith">Faith</Form.Label>
        <Form.Control
          type="text"
          id="createCharacter-faith"
          className="lowerCaseFont mb-4"
          name="faith"
          value={character.faith}
          required
          placeholder="Your faith"
          onChange={(e) => handleInputChange(e)}
        />
      </Form.Group>
      <Form.Group>
        <Form.Label htmlFor="createCharacter-backstory">Backstory</Form.Label>
        <Form.Control
          as="textarea"
          id="creteCharacter-backstory"
          className="lowerCaseFont mb-4"
          name="backstory"
          value={character.backstory}
          required
          placeholder="Whats your story?"
          onChange={(e) => handleInputChange(e)}
        />
      </Form.Group>
      <Container className="mt-3">
        <Image
          className="mb-4 mt-3 campaignDescription-image"
          src={uploadedImage}
          width={300}
        />
      </Container>
      <Row className="mb-4">
        <Col>
          <CloudinaryUploadWidget
            uploadedImage={uploadedImage}
            setUploadedImage={setUploadedImage}
            darkMode={darkMode}
          />
        </Col>
        {uploadedImage && (
          <Col>
            <Button className="btn-primary" onClick={handleRemoveImage}>
              Remove Image
            </Button>
          </Col>
        )}
      </Row>
      <Row>
        <Col>
          <Button
            className="btn-primary"
            disabled={buttonIsDisabled}
            onClick={() => {
              setStep(3);
              setObjectLength(15);
            }}
          >
            <FontAwesomeIcon icon="fa-solid fa-arrow-left" />
          </Button>
        </Col>
        <Col>
          <Button
            className="btn-primary"
            disabled={buttonIsDisabled}
            onClick={() => handleCreateCharacter()}
          >
            Submit
          </Button>
        </Col>
      </Row>
    </Form>
  );
};
