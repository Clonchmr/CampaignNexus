import {
  Button,
  Container,
  Image,
  Nav,
  Navbar,
  NavDropdown,
  Offcanvas,
} from "react-bootstrap";
import { NavLink } from "react-router-dom";
import logo from "../assets/images/CampaignNexusLogo.webp";
import { logout } from "../managers/authManager";

export const NavBar = ({ loggedInUser, setLoggedInUser }) => {
  return (
    <Navbar
      bg="light"
      data-bs-theme="light"
      expand="lg"
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
              </NavDropdown>
              <Nav.Link href="/campaigns/create">Create Campaign</Nav.Link>
            </Nav>
            <Nav>
              {loggedInUser ? (
                <Button
                  variant="outline-danger"
                  onClick={() => logout().then(() => setLoggedInUser(null))}
                >
                  Logout
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
