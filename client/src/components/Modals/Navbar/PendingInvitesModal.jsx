import { Button, Card, Col, Modal, Row } from "react-bootstrap";
import { InviteDetailsModal } from "./InviteDetailsModal";
import { useState } from "react";
import {
  declineInvite,
  getPendingInvites,
} from "../../../managers/invitationManager";

export const PendingInvitesModal = ({
  pendingInvitesModal,
  pendingInvitesToggle,
  userPendingInvites,
  setUserPendingInvites,
  darkMode,
  inviteDetailsModal,
  inviteDetailsToggle,
  loggedInUser,
}) => {
  const [detailsModalTarget, setDetailsModalTarget] = useState({});

  const handleDeclineInvite = async (inviteId) => {
    await declineInvite(inviteId);
    const updatedInvites = await getPendingInvites(loggedInUser.id, null);
    setUserPendingInvites(updatedInvites);

    if (inviteDetailsModal) {
      inviteDetailsToggle();
    }
  };

  return (
    <Modal
      show={pendingInvitesModal}
      onHide={pendingInvitesToggle}
      size="lg"
      data-bs-theme={darkMode ? "dark" : "light"}
    >
      <Modal.Header closeButton>Your Pending Invites</Modal.Header>
      <Modal.Body>
        {userPendingInvites.length > 0 ? (
          userPendingInvites.map((i) => (
            <Card key={i.id} className="mb-4">
              <Card.Body>
                <Row className="d-flex">
                  <Col
                    md={4}
                    className=" d-flex flex-column align-items-start justify-content-between"
                  >
                    <p style={{ fontSize: "1.2rem" }} className="mb-5">
                      {i.sender?.userName}
                    </p>
                    <Button
                      className="btn-primary"
                      style={{ fontSize: "0.7rem" }}
                      onClick={() => {
                        setDetailsModalTarget(i);
                        inviteDetailsToggle();
                      }}
                    >
                      View Invitation
                    </Button>
                  </Col>
                  <Col md={4} className="d-flex justify-content-center">
                    <p style={{ fontSize: "1rem" }}>
                      {i.campaign?.campaignName}
                    </p>
                  </Col>
                  <Col
                    md={4}
                    className=" d-flex flex-column align-items-end justify-content-between"
                  >
                    <p style={{ fontSize: "1.2rem" }}>
                      {i.dateSent?.split("T")[0]}
                    </p>
                    <Button
                      className="btn-primary"
                      style={{ fontSize: "0.7rem" }}
                      onClick={() => handleDeclineInvite(i.id)}
                    >
                      Decline
                    </Button>
                  </Col>
                </Row>
              </Card.Body>
            </Card>
          ))
        ) : (
          <p>You have no pending invites</p>
        )}
      </Modal.Body>
      <InviteDetailsModal
        darkMode={darkMode}
        inviteDetailsModal={inviteDetailsModal}
        inviteDetailsToggle={inviteDetailsToggle}
        pendingInvitesToggle={pendingInvitesToggle}
        detailsModalTarget={detailsModalTarget}
        loggedInUser={loggedInUser}
        userPendingInvites={userPendingInvites}
        setUserPendingInvites={setUserPendingInvites}
        handleDeclineInvite={handleDeclineInvite}
      />
    </Modal>
  );
};
