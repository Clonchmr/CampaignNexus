import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import {
  completeCampaign,
  deleteCampaign,
  getCampaignById,
} from "../../managers/campaignManager";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  Button,
  Card,
  Col,
  Container,
  Form,
  Image,
  ListGroup,
  Modal,
  Row,
} from "react-bootstrap";
import "../../styles/campaign.css";
import { newCampaignLog } from "../../managers/campaignLogManager";
import { RenderLogs } from "../CampaignLog/RenderLogs";
import { getAllUserProfiles } from "../../managers/userProfileManager";
import {
  deleteInvitation,
  getPendingInvites,
  sendInvite,
} from "../../managers/invitationManager";
import { getCharacters } from "../../managers/characterManager";
import { removeCharacterFromCampaign } from "../../managers/characterCampaignManager";

export const CampaignDetails = ({ loggedInUser, darkMode }) => {
  const [campaign, setCampaign] = useState([]);
  const [newLog, setNewLog] = useState({});
  const [logModel, setLogModel] = useState(false);
  const [deleteModal, setDeleteModal] = useState(false);
  const [logToEdit, setLogToEdit] = useState(null);
  const [isDisabled, setIsDisabled] = useState(true);
  const [inviteModal, setInviteModal] = useState(false);
  const [users, setUsers] = useState([]);
  const [userSearch, setUserSearch] = useState("");
  const [filteredUsers, setFilteredUsers] = useState([]);
  const [userDropdownChoice, setUserDropdownChoice] = useState(0);
  const [pendingInvites, setPendingInvites] = useState([]);
  const [pendingInvitesModal, setPendingInvitesModal] = useState(false);
  const [campaignCharacters, setCampaignCharacters] = useState([]);

  const { id } = useParams();

  const navigate = useNavigate();

  const logToggle = () => setLogModel(!logModel);
  const deleteToggle = () => setDeleteModal(!deleteModal);
  const inviteToggle = () => {
    setInviteModal(!inviteModal);
    setUserSearch("");
    setUserDropdownChoice(0);
  };
  const pendingInvitesToggle = () =>
    setPendingInvitesModal(!pendingInvitesModal);

  const fetchCampaign = (campaignId) => {
    getCampaignById(campaignId).then(setCampaign);
  };

  useEffect(() => {
    fetchCampaign(id);
    getAllUserProfiles().then(setUsers);
    getCharacters(null, id).then(setCampaignCharacters);
  }, [id, campaignCharacters.length]);

  useEffect(() => {
    getPendingInvites(null, id).then(setPendingInvites);
  }, [id, pendingInvites.length]);

  useEffect(() => {
    if (deleteModal) {
      setIsDisabled(true);
    }
    const timer = setTimeout(() => {
      setIsDisabled(false);
    }, 5000);

    return () => clearTimeout(timer);
  }, [deleteModal]);

  const handleNewLog = () => {
    const campaignLogObj = {
      campaignId: id,
      title: newLog.title,
      body: newLog.body,
    };

    newCampaignLog(campaignLogObj).then(() => {
      fetchCampaign(id);
      logToggle();
      setNewLog({});
    });
  };

  const handleCompleteCampaign = () => {
    completeCampaign(id).then(() => fetchCampaign(id).then(() => setCampaign));
  };

  const handleDeleteCampaign = () => {
    deleteCampaign(id).then(() => navigate("/campaigns"));
    logToggle();
  };

  const handleUserSearch = (e) => {
    const value = e.target.value;
    setUserSearch(value);

    if (value) {
      setFilteredUsers(
        users.filter((user) =>
          user.userName.toLowerCase().includes(value.toLowerCase())
        )
      );
    } else {
      setFilteredUsers([]);
    }
  };

  const handleSelect = (user) => {
    setUserSearch({ id: user.id, userName: user.userName });
    setFilteredUsers([]);
  };

  const handleSendInvite = () => {
    let recipientValue = 0;

    if (
      typeof userSearch === "object" &&
      userSearch !== null &&
      "id" in userSearch
    ) {
      recipientValue = userSearch.id;
    } else if (userSearch !== "") {
      const foundUser = users.find((u) => u.userName === userSearch);
      if (foundUser) {
        recipientValue = foundUser.id;
      }
    } else {
      recipientValue = userDropdownChoice;
    }

    const invite = {
      senderId: campaign.ownerId,
      recipientId: recipientValue,
      campaignId: parseInt(id),
    };

    sendInvite(invite).then(() => {
      getPendingInvites(null, id).then(setPendingInvites);
    });
    inviteToggle();
  };

  const handleDeleteInvite = (inviteId) => {
    deleteInvitation(inviteId).then(() => {
      getPendingInvites(null, id).then(setPendingInvites);
    });
  };

  const handleRemoveCharacter = (characterId, campaignId) => {
    removeCharacterFromCampaign(characterId, campaignId).then(() => {
      getCharacters(null, id).then(setCampaignCharacters);
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
            <Image
              src={campaign.campaignPicUrl}
              style={{ maxWidth: "20rem" }}
              id="campaignDescription-image"
            />
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
              <Button className="btn-primary" onClick={inviteToggle}>
                Invite New Player
              </Button>
            </Col>
            <Col>
              <Button className="btn-primary" onClick={pendingInvitesToggle}>
                See Pending Invites
              </Button>
            </Col>
          </Row>
        )}
        {campaignCharacters != null && (
          <Row className="campaignPlayers-container mt-5">
            <Col>
              {campaignCharacters.map((character, index) =>
                index % 2 === 0 ? (
                  <Card
                    key={character.id}
                    className="campaignDetails-container"
                  >
                    <Row>
                      <Col>
                        <Card.Img
                          src={character.characterPicUrl}
                          style={{
                            maxWidth: "7rem",
                            height: "8rem",
                          }}
                        />
                      </Col>
                      <Col className="d-flex align-items-center">
                        <Card.Body>
                          <Card.Title className="primaryText-color">
                            {character.name}
                          </Card.Title>
                        </Card.Body>
                      </Col>
                      {(loggedInUser.id === campaign.ownerId ||
                        loggedInUser.id === character.userId) && (
                        <Col className="d-flex align-items-center">
                          <Button
                            className="btn-primary"
                            onClick={() =>
                              handleRemoveCharacter(character.id, id)
                            }
                          >
                            Remove
                          </Button>
                        </Col>
                      )}
                    </Row>
                  </Card>
                ) : null
              )}
            </Col>
            <Col>
              {campaignCharacters.map((character, index) =>
                index % 2 !== 0 ? (
                  <Card
                    key={character.id}
                    className="campaignDetails-container"
                  >
                    <Row>
                      <Col>
                        <Card.Img
                          src={character.characterPicUrl}
                          style={{
                            maxWidth: "7rem",
                            height: "8rem",
                          }}
                        />
                      </Col>
                      <Col className="d-flex align-items-center">
                        <Card.Body>
                          <Card.Title className="primaryText-color">
                            {character.name}
                          </Card.Title>
                        </Card.Body>
                      </Col>
                      {(loggedInUser.id === campaign.ownerId ||
                        loggedInUser.id === character.userId) && (
                        <Col className="d-flex align-items-center">
                          <Button
                            className="btn-primary"
                            onClick={() =>
                              handleRemoveCharacter(character.id, id)
                            }
                          >
                            Remove
                          </Button>
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

        <RenderLogs
          loggedInUser={loggedInUser}
          logToEdit={logToEdit}
          setLogToEdit={setLogToEdit}
          campaign={campaign}
          darkMode={darkMode}
          logToggle={logToggle}
          fetchCampaign={fetchCampaign}
        />

        <Row className="campaignDetails-footer mt-5">
          {loggedInUser.id === campaign.ownerId &&
            campaign.endDate === null && (
              <Col>
                <Button
                  className="btn-primary"
                  onClick={handleCompleteCampaign}
                >
                  Complete Campaign
                </Button>
              </Col>
            )}
          <Col>
            {loggedInUser.id === campaign.ownerId ? (
              <Button className="btn-primary" onClick={deleteToggle}>
                Delete Campaign
              </Button>
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
                className="lowerCaseFont"
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
      <Modal
        show={deleteModal}
        onHide={deleteToggle}
        data-bs-theme={darkMode ? "dark" : "light"}
      >
        <Modal.Header closeButton></Modal.Header>
        <Modal.Body>
          <p>Delete your campaign: {campaign.campaignName}?</p>
          <p>This action cannot be reversed.</p>
        </Modal.Body>
        <Modal.Footer>
          <Button
            disabled={isDisabled}
            className="btn-primary"
            onClick={handleDeleteCampaign}
          >
            Delete
          </Button>
          <Button className="btn-primary" onClick={deleteToggle}>
            Cancel
          </Button>
        </Modal.Footer>
      </Modal>
      <Modal
        show={inviteModal}
        onHide={inviteToggle}
        data-bs-theme={darkMode ? "dark" : "light"}
      >
        <Modal.Header closeButton>
          Send a new invitation to this campaign
        </Modal.Header>
        <Modal.Body>
          <Form>
            <Form.Group className="mb-3">
              <Form.Label htmlFor="inviteUSer-dropdown">
                Invite by dropdown
              </Form.Label>
              <Form.Select
                aria-label="inviteUser-dropdown"
                className="lowerCaseFont"
                disabled={userSearch !== ""}
                onChange={(e) =>
                  setUserDropdownChoice(parseInt(e.target.value))
                }
              >
                <option value={0}>Select User</option>
                {users.map((u) => (
                  <option key={u.id} value={u.id}>
                    {u.userName}
                  </option>
                ))}
              </Form.Select>
            </Form.Group>
            <Form.Group>
              <Form.Label htmlFor="inviteUser-search">User Name</Form.Label>
              <Form.Control
                className="lowerCaseFont"
                type="text"
                disabled={userDropdownChoice !== 0}
                placeholder="Search by user name"
                value={userSearch?.userName}
                onChange={(e) => handleUserSearch(e)}
              />
              {filteredUsers.length > 0 && (
                <ListGroup className="lowerCaseFont">
                  {filteredUsers.map((u) => (
                    <ListGroup.Item
                      key={u.id}
                      action
                      onClick={() => handleSelect(u)}
                    >
                      {u.userName}
                    </ListGroup.Item>
                  ))}
                </ListGroup>
              )}
            </Form.Group>
          </Form>
        </Modal.Body>
        <Modal.Footer>
          <Button className="btn-primary" onClick={handleSendInvite}>
            Send Invite
          </Button>
          <Button
            className="btn-primary"
            onClick={() => {
              inviteToggle();
              setFilteredUsers([]);
            }}
          >
            Cancel
          </Button>
        </Modal.Footer>
      </Modal>
      <Modal
        show={pendingInvitesModal}
        onHide={pendingInvitesToggle}
        data-bs-theme={darkMode ? "dark" : "light"}
      >
        <Modal.Header closeButton>Pending Invites</Modal.Header>
        <Modal.Body>
          {pendingInvites.map((i) => (
            <Card
              key={i.id}
              className="lowerCaseFont-bold mb-3"
              style={{ boxShadow: "0 0 0.1rem white" }}
            >
              <Card.Body>
                <Row className="d-flex justify-content-around">
                  <Col
                    md={6}
                    className="d-flex flex-column justify-content-around"
                  >
                    <div className="mb-4">{`${i.recipient?.userName}`}</div>
                    <div>{campaign.campaignName}</div>
                  </Col>
                  <Col
                    md={6}
                    className="d-flex flex-column justify-content-around"
                  >
                    <div className="mb-4">{i.dateSent?.split("T")[0]}</div>
                    <div>
                      <Button
                        className="btn-primary"
                        onClick={() => handleDeleteInvite(i.id)}
                      >
                        Cancel Invite
                      </Button>
                    </div>
                  </Col>
                </Row>
              </Card.Body>
            </Card>
          ))}
        </Modal.Body>
      </Modal>
    </Container>
  );
};
