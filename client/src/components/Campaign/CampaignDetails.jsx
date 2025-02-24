import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { getCampaignById } from "../../managers/campaignManager";
//import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  Button,
  Card,
  Col,
  Container,
  Form,
  Image,
  Modal,
  Row,
} from "react-bootstrap";
import "../../styles/campaign.css";
import { newCampaignLog } from "../../managers/campaignLogManager";

export const CampaignDetails = ({ loggedInUser, darkMode }) => {
  const [campaign, setCampaign] = useState([]);
  const [newLog, setNewLog] = useState({});
  const [logModel, setLogModel] = useState(false);

  const { id } = useParams();

  const navigate = useNavigate();

  const logToggle = () => setLogModel(!logModel);

  useEffect(() => {
    getCampaignById(id).then(setCampaign);
  }, [id]);

  const handleNewLog = () => {
    const campaignLogObj = {
      campaignId: id,
      title: newLog.title,
      body: newLog.body,
    };

    newCampaignLog(campaignLogObj).then(() => {
      logToggle();
      setNewLog({});
    });
  };
  return (
    <Container>
      <Container className="campaignDetails-container mt-5">
        {campaign.ownerId === loggedInUser.id && (
          <Button
            id="campaign-edit-btn"
            className="btn-primary"
            onClick={() => navigate(`/campaigns/${campaign.id}/edit`)}
          >
            Edit Campaign
          </Button>
        )}
        <Row className="campaignDetails-header d-flex mt-3">
          <Col>
            <Image src={campaign.campaignPicUrl} />
          </Col>
          <Col className="d-flex flex-column justify-content-around">
            <h3>{campaign.campaignName}</h3>
            <h6>{`Levels: ${campaign.levelRange}`}</h6>
            <h6>{`Started on: ${campaign.startDate?.split("T")[0]}`}</h6>
            <h6>
              {campaign.endDate !== null
                ? `Ended on: ${campaign.endDate?.split("T")[0]}`
                : "Active"}
            </h6>
          </Col>
        </Row>
        {campaign.ownerId === loggedInUser.id && (
          <Row className="campaignDetails-invite-btns mt-4">
            <Col>
              <Button className="btn-primary">Invite New Player</Button>
            </Col>
            <Col>
              <Button className="btn-primary">See Pending Invites</Button>
            </Col>
          </Row>
        )}
        {campaign.characters != null && (
          <Row className="campaignPlayers-container">
            <Col>
              {campaign.characters.map((character, index) =>
                index % 2 === 0 ? (
                  <Card key={character.id}>
                    <Row>
                      <Col>
                        <Card.Img src={character.characterPicUrl} />
                      </Col>
                      <Col>
                        <Card.Body>
                          <Card.Title>{character.name}</Card.Title>
                        </Card.Body>
                      </Col>
                      {loggedInUser.id === campaign.ownerId && (
                        <Col>
                          <Button className="btn-primary">Remove</Button>
                        </Col>
                      )}
                    </Row>
                  </Card>
                ) : null
              )}
            </Col>
            <Col>
              {campaign.characters.map((character, index) =>
                index % 2 !== 0 ? (
                  <Card key={character.id}>
                    <Row>
                      <Col>
                        <Card.Img src={character.characterPicUrl} />
                      </Col>
                      <Col>
                        <Card.Body>
                          <Card.Title>{character.name}</Card.Title>
                        </Card.Body>
                      </Col>
                      {loggedInUser.id === campaign.ownerId && (
                        <Col>
                          <Button className="btn-primary">Remove</Button>
                        </Col>
                      )}
                    </Row>
                  </Card>
                ) : null
              )}
            </Col>
          </Row>
        )}
        <Container className="campaignDetails-description-container mt-5">
          <p>{campaign.campaignDescription}</p>
        </Container>
        <Row
          className="campaignDetails-logs-container mt-5"
          style={{ width: "75%", margin: "auto" }}
        >
          <Col md={10}>
            <h4 id="campaign-logs-h4">Campaign Logs</h4>
          </Col>
          <Col md={2}>
            <Button className="btn-primary" onClick={logToggle}>
              Add Log
            </Button>
          </Col>
          {campaign.campaignLogs?.length > 0 ? (
            campaign.campaignLogs?.map((log) => (
              <Card key={log.id} className="card-background mt-4">
                <Card.Header>{log.title}</Card.Header>
                <Card.Body>
                  <Card.Text>
                    {log.body.length > 100
                      ? `${log.body?.slice(0, 100)}...`
                      : log.body}
                  </Card.Text>
                </Card.Body>
                <Card.Footer>{log.date?.split("T")[0]}</Card.Footer>
              </Card>
            ))
          ) : (
            <p>No Logs currently recorded for this campaign</p>
          )}
        </Row>
        <Row className="campaignDetails-footer mt-5">
          {loggedInUser.id === campaign.ownerId && (
            <Col>
              <Button className="btn-primary">Complete Campaign</Button>
            </Col>
          )}
          <Col>
            {loggedInUser.id === campaign.ownerId ? (
              <Button className="btn-primary">Delete Campaign</Button>
            ) : campaign.characters?.some(
                (c) => c.userId === loggedInUser.id
              ) ? (
              <Button className="btn-primary">Leave Campaign</Button>
            ) : (
              ""
            )}
          </Col>
        </Row>
      </Container>
      <Modal
        show={logModel}
        onHide={logToggle}
        data-bs-theme={darkMode ? "dark" : "light"}
      >
        <Modal.Header closeButton>
          <Modal.Title>Add a Log</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <Form.Group controlId="newLog-title" className="mb-4">
              <Form.Label>Title</Form.Label>
              <Form.Control
                type="text"
                required
                name="title"
                placeholder="Enter Title"
                onChange={(e) => {
                  setNewLog({ ...newLog, title: e.target.value });
                }}
              />
            </Form.Group>
            <Form.Group controlId="newLog-body">
              <Form.Label>Body</Form.Label>
              <Form.Control
                as="textarea"
                required
                placeholder="Log Body"
                onChange={(e) => {
                  setNewLog({ ...newLog, body: e.target.value });
                }}
              />
            </Form.Group>
          </Form>
        </Modal.Body>
        <Modal.Footer>
          <Button className="btn-primary" onClick={handleNewLog}>
            Add Log
          </Button>
          <Button className="btn-primary" onClick={logToggle}>
            Cancel
          </Button>
        </Modal.Footer>
      </Modal>
    </Container>
  );
};
