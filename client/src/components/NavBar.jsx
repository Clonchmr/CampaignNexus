import {
  Badge,
  Button,
  Container,
  Form,
  Image,
  Nav,
  Navbar,
  NavDropdown,
  Offcanvas,
  OverlayTrigger,
  Tooltip,
} from "react-bootstrap";
import { NavLink } from "react-router-dom";
import logo from "../assets/images/CampaignNexusLogo.webp";
import { logout } from "../managers/authManager";
import { useEffect, useState } from "react";
import { getCampaignsByUser } from "../managers/campaignManager";
import "../styles/nav.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { getPendingInvites } from "../managers/invitationManager";
import { PendingInvitesModal } from "./Modals/Navbar/PendingInvitesModal";

export const NavBar = ({
  loggedInUser,
  setLoggedInUser,
  darkMode,
  setDarkMode,
}) => {
  const [userCampaigns, setUserCampaigns] = useState([]);
  const [userPendingInvites, setUserPendingInvites] = useState([]);
  const [pendingInvitesModal, setPendingInvitesModal] = useState(false);
  const [inviteDetailsModal, setInviteDetailsModal] = useState(false);

  const themeClass = darkMode ? "dark" : "light";

  const pendingInvitesToggle = () =>
    setPendingInvitesModal(!pendingInvitesModal);
  const inviteDetailsToggle = () => setInviteDetailsModal(!inviteDetailsModal);

  const renderInviteTooltip = (props) => (
    <Tooltip id="invite-tooltip" {...props}>
      Pending Invitations
    </Tooltip>
  );

  useEffect(() => {
    getCampaignsByUser(loggedInUser.id, 3, true).then(setUserCampaigns);
    getPendingInvites(loggedInUser.id, null).then(setUserPendingInvites);
  }, [loggedInUser.id]);
  return (
    <Navbar
      expand="lg"
      bg={themeClass}
      data-bs-theme={themeClass}
      fixed="top"
      className="fs-4"
    >
      <Container fluid>
        <Navbar.Brand as={NavLink} to="/" className="mx-5">
          <Image src={logo} style={{ width: "55px" }} />
        </Navbar.Brand>
        <Navbar.Toggle aria-controls="offcanvasNavbar" />
        <Navbar.Offcanvas
          id="offcanvasNavbar"
          aria-labelledby="offcanvasNavbarLabel"
          placement="end"
          className={darkMode ? "offcanvas-dark" : "offcanvas-light"}
        >
          <Offcanvas.Header closeButton>
            <Offcanvas.Title id="offcanvasNavbar-title">
              CampaignNexus
            </Offcanvas.Title>
          </Offcanvas.Header>
          <Offcanvas.Body>
            <Nav className="me-auto flex-grow-1 pe-3">
              <NavDropdown title="Campaigns" id="campaignsDropdown">
                <NavDropdown.Item href="/campaigns">View All</NavDropdown.Item>
                {userCampaigns.map((c) => (
                  <NavDropdown.Item
                    key={`Campaign-${c.id}`}
                    href={`/campaigns/${c.id}`}
                  >
                    {c.campaignName}
                  </NavDropdown.Item>
                ))}
              </NavDropdown>
              <Nav.Link href="/campaigns/create">Create Campaign</Nav.Link>
              <Nav.Link as="span">
                <OverlayTrigger
                  placement="right"
                  delay={{ show: 250, hide: 500 }}
                  overlay={renderInviteTooltip}
                >
                  <Button
                    variant={darkMode ? "dark" : "light"}
                    onClick={pendingInvitesToggle}
                  >
                    <FontAwesomeIcon icon="fa-solid fa-bell" />
                    {userPendingInvites.length > 0 && (
                      <Badge
                        bg="#6e0d25"
                        style={{
                          color: darkMode ? "white" : "black",
                          fontFamily: "serif",
                        }}
                      >
                        {userPendingInvites.length}
                      </Badge>
                    )}
                  </Button>
                </OverlayTrigger>
              </Nav.Link>
            </Nav>
            <Nav className="me-auto">
              <Form>
                <Form.Check
                  type="switch"
                  id="darkMode-switch"
                  checked={darkMode}
                  onChange={() => setDarkMode(!darkMode)}
                  label={darkMode ? "🌙" : "🔆"}
                />
              </Form>
              {loggedInUser ? (
                <Button
                  className="btn-primary"
                  onClick={() => logout().then(() => setLoggedInUser(null))}
                >
                  Log Out
                </Button>
              ) : (
                <Nav.Link as={NavLink} href="/login">
                  <Button variant="outline-primary">Login</Button>
                </Nav.Link>
              )}
            </Nav>
          </Offcanvas.Body>
        </Navbar.Offcanvas>
      </Container>
      <PendingInvitesModal
        pendingInvitesModal={pendingInvitesModal}
        pendingInvitesToggle={pendingInvitesToggle}
        userPendingInvites={userPendingInvites}
        setUserPendingInvites={setUserPendingInvites}
        darkMode={darkMode}
        inviteDetailsModal={inviteDetailsModal}
        inviteDetailsToggle={inviteDetailsToggle}
        loggedInUser={loggedInUser}
      />
    </Navbar>
  );
};
