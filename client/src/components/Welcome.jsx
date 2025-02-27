import { Button, Col, Container, Image, Row } from "react-bootstrap";
import logo from "../assets/images/CampaignNexusLogo.webp";
import { useNavigate } from "react-router-dom";

export const Welcome = ({ loggedInUser }) => {
  const navigate = useNavigate();

  return (
    <Container>
      <Row className="welcome-container mt-5 campaignDetails-container">
        <Col>
          <h2 className="mt-5 mb-5">Welcome to Campaign Nexus</h2>
          <p>
            Your ultimate companion for Dungeons & Dragons adventures! Whether
            you're crafting a heroic character, managing an epic campaign, or
            bringing your party together, CampaignNexus streamlines your journey
            through the realms of imagination. With intuitive tools for
            character creation, campaign organization, and party management, you
            can focus on storytelling and strategy while we handle the details.
            Step into a world where your adventures come to life—your next great
            quest begins here!
          </p>
        </Col>
        <Col className="mt-5">
          <Image
            alt="Campaign Nexus logo"
            src={logo}
            className="mb-5 campaignDescription-image "
            style={{ maxWidth: "20rem" }}
          />
        </Col>
        <Row className="welcome-footer">
          <Col>
            <Button
              className="btn-primary"
              onClick={() => navigate("/campaigns/create")}
            >
              Start a campaign
            </Button>
          </Col>
          <Col>
            <Button className="btn-primary">Create a Character</Button>
          </Col>
        </Row>
      </Row>
    </Container>
  );
};
