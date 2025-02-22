import { useEffect, useState } from "react";
import { getCampaignsByUser } from "../../managers/campaignManager";
import { Button, Card, Col, Container, Row } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import "../../styles/campaign.css";

export const UserCampaigns = ({ loggedInUser }) => {
  const [campaigns, setCampaigns] = useState([]);
  const [buttonIsClicked, setButtonIsClicked] = useState(false);

  const navigate = useNavigate();

  useEffect(() => {
    getCampaignsByUser(loggedInUser.id).then(setCampaigns);
  }, [loggedInUser]);

  useEffect(() => {
    if (buttonIsClicked) {
      getCampaignsByUser(loggedInUser.id, null, true).then(setCampaigns);
    } else {
      getCampaignsByUser(loggedInUser.id).then(setCampaigns);
    }
  }, [buttonIsClicked, loggedInUser]);

  const handleToggleCheck = () => {
    setButtonIsClicked(!buttonIsClicked);
  };
  return (
    <Container>
      <Container
        className="userCampaigns-header-container mt-5"
        style={{ width: "40rem" }}
      >
        <Row className="d-flex">
          <Col className="d-flex justify-content-start">
            <Button className="btn-primary">Start New</Button>
          </Col>
          <Col className="d-flex justify-content-end">
            <Button className="btn-primary" onClick={handleToggleCheck}>
              {buttonIsClicked ? "Show All" : "Show Active"}
            </Button>
          </Col>
        </Row>
      </Container>
      <Container className="userCampaigns-card-container">
        {campaigns.map((c) => (
          <Card
            key={c.id}
            className="card-background card-text mt-5 mb-3 card-border card-hover"
            style={{ height: "100%" }}
            onClick={() => navigate(`/campaigns/${c.id}`)}
          >
            <Row className="d-flex ">
              <Col>
                <Card.Img
                  src={c.campaignPicUrl}
                  style={{ objectFit: "cover" }}
                />
              </Col>
              <Col className="d-flex flex-column justify-content-around">
                <Card.Body className="d-flex flex-column justify-content-around">
                  <Card.Title>{c.campaignName}</Card.Title>
                  <Card.Text>
                    Date Started: {c.startDate?.split("T")[0]}
                  </Card.Text>
                </Card.Body>
              </Col>
              <Col className="d-flex flex-column justify-content-around ">
                <Card.Text>
                  {c.endDate
                    ? `Date Ended: ${c.endDate?.split("T")[0]}`
                    : "Active"}
                </Card.Text>
                <Card.Text>
                  {c.characters?.length === 0
                    ? "No active players"
                    : `${c.characters?.length} Players`}
                </Card.Text>
              </Col>
            </Row>
          </Card>
        ))}
      </Container>
    </Container>
  );
};
