import { useEffect, useState } from "react";
import { Button, Container, Form, Image, Modal } from "react-bootstrap";
import { getCharacters } from "../../../managers/characterManager";
import {
  acceptInvite,
  getPendingInvites,
} from "../../../managers/invitationManager";
import { useNavigate } from "react-router-dom";

export const InviteDetailsModal = ({
  darkMode,
  inviteDetailsModal,
  inviteDetailsToggle,
  pendingInvitesToggle,
  detailsModalTarget,
  loggedInUser,
  setUserPendingInvites,
  handleDeclineInvite,
}) => {
  const [chosenCharacter, setChosenCharacter] = useState(null);
  const [userCharacters, setUserCharacters] = useState([]);

  const navigate = useNavigate();

  useEffect(() => {
    getCharacters(loggedInUser.id, null).then(setUserCharacters);
  }, [loggedInUser]);

  const target = detailsModalTarget.campaign;

  const handleAcceptInvite = async () => {
    const characterCampaignObj = {
      campaignId: target?.id,
      characterId: parseInt(chosenCharacter),
    };

    await acceptInvite(characterCampaignObj);

    const updatedInvites = await getPendingInvites(loggedInUser.id);

    setUserPendingInvites(updatedInvites);

    inviteDetailsToggle();
    pendingInvitesToggle();
    navigate(`/campaigns/${target.id}`);
  };

  return (
    <Modal
      show={inviteDetailsModal}
      onHide={inviteDetailsToggle}
      size="xl"
      data-bs-theme={darkMode ? "dark" : "light"}
      className="text-center"
    >
      <Modal.Header closeButton></Modal.Header>
      <Modal.Body>
        <Container>
          <Image
            alt={`Image for the campaign ${target?.campaignname}`}
            src={target?.campaignPicUrl}
            className="mb-4"
          />
          <h3 className="mb-4">{target?.campaignName}</h3>
          <h5 className="mb-4">{`Owner: ${detailsModalTarget.sender?.userName}`}</h5>
          <h6 className="mb-4">{`Level Range: ${target?.levelRange}`}</h6>
          <p className="mb-4">{target?.campaignDescription}</p>
          <Form
            style={{ width: "50%", marginLeft: "auto", marginRight: "auto" }}
          >
            <Form.Group>
              <Form.Label htmlFor="accept-invite-character">
                Character To Join With
              </Form.Label>
              <Form.Select
                aria-label="accept-invite-character"
                onChange={(e) =>
                  e.target.value !== ""
                    ? setChosenCharacter(e.target.value)
                    : setChosenCharacter(null)
                }
              >
                <option value="">Choose Character</option>
                {userCharacters.map((c) => (
                  <option key={c.id} value={c.id}>
                    {c.name}
                  </option>
                ))}
              </Form.Select>
            </Form.Group>
          </Form>
        </Container>
      </Modal.Body>
      <Modal.Footer>
        <Button
          className="btn-primary"
          disabled={chosenCharacter === null}
          onClick={handleAcceptInvite}
        >
          Accept Invitation
        </Button>
        <Button
          className="btn-primary"
          onClick={() => handleDeclineInvite(detailsModalTarget.id)}
        >
          Decline Invitation
        </Button>
      </Modal.Footer>
    </Modal>
  );
};
