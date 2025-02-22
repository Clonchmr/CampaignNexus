import {
  Button,
  Container,
  Form,
  Image,
  Nav,
  Navbar,
  NavDropdown,
  Offcanvas,
} from "react-bootstrap";
import { NavLink } from "react-router-dom";
import logo from "../assets/images/CampaignNexusLogo.webp";
import { logout } from "../managers/authManager";
import { useContext, useEffect, useState } from "react";
import { ThemeContext } from "../ThemeContext/ThemeContext";
import { getCampaignsByUser } from "../managers/campaignManager";

export const NavBar = ({ loggedInUser, setLoggedInUser }) => {
  const { darkMode, setDarkMode } = useContext(ThemeContext);
  const [userCampaigns, setUserCampaigns] = useState([]);

  const themeClass = darkMode ? "dark" : "light";

  useEffect(() => {
    getCampaignsByUser(loggedInUser.id, 3, true).then(setUserCampaigns);
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
    </Navbar>
  );
};
