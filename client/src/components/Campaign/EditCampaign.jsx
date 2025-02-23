import { useEffect, useRef, useState } from "react";
import { editCampaign, getCampaignById } from "../../managers/campaignManager";
import { useNavigate, useParams } from "react-router-dom";
import { Button, Col, Container, Form, Image, Row } from "react-bootstrap";
import { CloudinaryUploadWidget } from "../CloudinaryUploadWidget";
import { deleteImage } from "../../managers/cloudinaryManager";

export const EditCampaign = ({ darkMode }) => {
  const [campaignToEdit, setCampaignToEdit] = useState({});
  const [imageUrl, setImageUrl] = useState(null);

  const { id } = useParams();

  const textAreaRef = useRef(null);

  const navigate = useNavigate();

  useEffect(() => {
    getCampaignById(id)
      .then((prev) => {
        setCampaignToEdit({
          ...prev,
          startingLevel: prev.levelRange.split(" - ")[0],
          endingLevel: prev.levelRange.split(" - ")[1],
        });
      })
      .then(() => {
        setImageUrl(campaignToEdit.campaignPicUrl);
      });
  }, [id, campaignToEdit.campaignPicUrl]);

  useEffect(() => {
    if (textAreaRef.current) {
      textAreaRef.current.style.height = "auto";
      textAreaRef.current.style.height = `${textAreaRef.current.scrollHeight}px`;
    }
  }, [campaignToEdit.campaignDescription]);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setCampaignToEdit((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleRemoveImage = () => {
    if (!imageUrl) return;

    //Extract public ID from url
    const publicId = imageUrl.split("/").pop().split(".")[0];

    const publicIdObj = {
      publicId: publicId,
    };
    deleteImage(publicIdObj).then(() => setImageUrl(null));
  };

  const handleEdit = (e) => {
    e.preventDefault();

    const editedCampaignObj = {
      id: campaignToEdit.id,
      campaignName: campaignToEdit.campaignName,
      levelRange: `${campaignToEdit.startingLevel} - ${campaignToEdit.endingLevel}`,
      campaignDescription: campaignToEdit.campaignDescription,
      campaignPicUrl: imageUrl,
    };

    editCampaign(editedCampaignObj).then(() =>
      navigate(`/campaigns/${campaignToEdit.id}`)
    );
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
            value={campaignToEdit.campaignName}
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
                value={campaignToEdit.startingLevel}
                name="startingLevel"
                data-bs-theme={darkMode ? "dark" : "light"}
                onChange={(e) => handleInputChange(e)}
              />
            </Col>
            <Col>
              <Form.Control
                type="number"
                required
                value={campaignToEdit.endingLevel}
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
            ref={textAreaRef}
            required
            name="campaignDescription"
            value={campaignToEdit.campaignDescription}
            data-bs-theme={darkMode ? "dark" : "light"}
            onChange={(e) => handleInputChange(e)}
          />
        </Form.Group>
        <Container className="mt-3">
          <Image className="mb-4 mt-3" src={imageUrl} width={200} />
        </Container>
        <Row className="mb-4">
          <Col>
            <CloudinaryUploadWidget
              uploadedImage={imageUrl}
              setUploadedImage={setImageUrl}
              darkMode={darkMode}
            />
          </Col>
          {imageUrl && imageUrl !== campaignToEdit.campaignPicUrl && (
            <Col>
              <Button className="btn-primary" onClick={handleRemoveImage}>
                Remove Image
              </Button>
            </Col>
          )}
        </Row>

        <Form.Group controlId="newCampaign-submitBtn">
          <Row>
            <Col>
              <Button
                type="submit"
                className="btn-primary"
                onClick={(e) => handleEdit(e)}
              >
                Save Changes
              </Button>
            </Col>
            <Col>
              <Button
                className="btn-primary"
                onClick={() => {
                  imageUrl !== campaignToEdit.campaignPicUrl &&
                    handleRemoveImage();
                  navigate(`/campaigns/${id}`);
                }}
              >
                Cancel
              </Button>
            </Col>
          </Row>
        </Form.Group>
      </Form>
    </Container>
  );
};
