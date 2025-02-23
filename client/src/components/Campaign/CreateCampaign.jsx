import { useState } from "react";
import { Button, Col, Container, Form, Image, Row } from "react-bootstrap";
import { CloudinaryUploadWidget } from "../CloudinaryUploadWidget";
import { deleteImage } from "../../managers/cloudinaryManager";
import { useNavigate } from "react-router-dom";
import { postNewCampaign } from "../../managers/campaignManager";

export const CreateCampaign = ({ loggedInUser, darkMode }) => {
  const [uploadedImage, setUploadedImage] = useState(null);
  const [newCampaign, setNewCampaign] = useState({});

  const navigate = useNavigate();

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setNewCampaign((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleRemoveImage = () => {
    if (!uploadedImage) return;

    //Extract public ID from url
    const publicId = uploadedImage.split("/").pop().split(".")[0];

    const publicIdObj = {
      publicId: publicId,
    };
    deleteImage(publicIdObj).then(() => setUploadedImage(null));
  };

  const handleCreateCampaign = (e) => {
    e.preventDefault();

    const newCampaignObj = {
      ownerId: loggedInUser.id,
      campaignName: newCampaign.campaignName,
      campaignDescription: newCampaign.campaignDescription,
      levelRange: `${newCampaign.startingLevel} - ${newCampaign.endingLevel}`,
      campaignPicUrl: uploadedImage,
    };

    postNewCampaign(newCampaignObj).then((response) => {
      navigate(`/campaigns/${response.id}`);
    });
  };

  return (
    <Container>
      <Form className="mt-5">
        <Form.Group controlId="newCampaign-name" className="mb-4">
          <Form.Label>Name</Form.Label>
          <Form.Control
            type="text"
            required
            name="campaignName"
            placeholder="Enter Campaign Name"
            data-bs-theme={darkMode ? "dark" : "light"}
            onChange={(e) => handleInputChange(e)}
          />
        </Form.Group>
        <Form.Group controlId="newCampaign-levelRange" className="mb-4">
          <Form.Label>Level Range</Form.Label>
          <Row>
            <Col>
              <Form.Control
                type="number"
                required
                placeholder="Starting Level"
                name="startingLevel"
                data-bs-theme={darkMode ? "dark" : "light"}
                onChange={(e) => handleInputChange(e)}
              />
            </Col>
            <Col>
              <Form.Control
                type="number"
                required
                placeholder="Ending Level"
                name="endingLevel"
                data-bs-theme={darkMode ? "dark" : "light"}
                onChange={(e) => handleInputChange(e)}
              />
            </Col>
          </Row>
        </Form.Group>
        <Form.Group controlId="newCampaign-description" className="mb-4">
          <Form.Label>Description</Form.Label>
          <Form.Control
            as="textarea"
            rows={4}
            required
            name="campaignDescription"
            placeholder="Describe your campaign"
            data-bs-theme={darkMode ? "dark" : "light"}
            onChange={(e) => handleInputChange(e)}
          />
        </Form.Group>
        <Container className="mt-3">
          <Image className="mb-4 mt-3" src={uploadedImage} width={200} />
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

        <Form.Group controlId="newCampaign-submitBtn">
          <Button
            type="submit"
            className="btn-primary"
            onClick={(e) => handleCreateCampaign(e)}
          >
            Submit
          </Button>
        </Form.Group>
      </Form>
    </Container>
  );
};
