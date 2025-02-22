import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getCampaignById } from "../../managers/campaignManager";
import { Button, Card, Col, Container, Image, Row } from "react-bootstrap";
import "../../styles/campaign.css";

export const CampaignDetails = ({ loggedInUser }) => {
  const [campaign, setCampaign] = useState([]);

  const { id } = useParams();

  useEffect(() => {
    getCampaignById(id).then(setCampaign);
  }, [id]);
  return (
    <Container>
      <Container className="campaignDetails-container mt-5">
        {campaign.ownerId === loggedInUser.id && (
          <Button id="campaign-edit-btn" className="btn-primary">
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
        <Row className="campaignDetails-logs-container mt-5">
          <Col md={10}>
            <h4 id="campaign-logs-h4">Campaign Logs</h4>
          </Col>
          <Col md={2}>
            <Button className="btn-primary">Add Log</Button>
          </Col>
          {campaign.campaignLogs?.length > 0 ? (
            campaign.campaignLogs?.map((log) => (
              <Card key={log.id} className="card-background">
                <Card.Header>{log.title}</Card.Header>
                <Card.Body>
                  <Card.Text>{log.body}</Card.Text>
                </Card.Body>
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
    </Container>
  );
};
