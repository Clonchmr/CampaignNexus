import { useEffect, useState } from "react";
import { getCharacters } from "../../managers/characterManager";
import { Button, Card, Col, Container, Row } from "react-bootstrap";
import { useNavigate } from "react-router-dom";

export const UserCharacters = ({ loggedInUser, darkMode }) => {
  const [characters, setCharacters] = useState([]);

  const navigate = useNavigate();

  useEffect(() => {
    getCharacters(loggedInUser.id).then(setCharacters);
  }, [loggedInUser]);
  return (
    <Container className="mt-5">
      <Container className="userCharacters-header">
        <Button
          className="btn-primary"
          onClick={() => navigate("/characters/create")}
        >
          Create New
        </Button>
      </Container>
      {characters.length > 0 ? (
        characters.map((c) => (
          <Card
            key={c.id}
            data-bs-theme={darkMode ? "dark" : "light"}
            className="card-background card-text mt-5 mb-3 card-border card-hover"
            onClick={() => navigate(`/characters/${c.id}`)}
          >
            <Row className="d-flex">
              <Col>
                <Card.Img
                  alt={`Character picture for ${c.name}`}
                  src={c.characterPicUrl}
                  style={{ maxWidth: "15rem" }}
                />
              </Col>
              <Col className="d-flex flex-column justify-content-around">
                <Card.Body className="d-flex flex-column justify-content-around">
                  <Card.Text>{c.name}</Card.Text>
                  <Card.Text>{`Level ${c.level}`}</Card.Text>
                </Card.Body>
              </Col>
              <Col className="d-flex flex-column justify-content-around">
                <Card.Body className="d-flex flex-column justify-content-around">
                  <Card.Text>{c.class?.className}</Card.Text>
                  <Card.Text>{c.species?.speciesName}</Card.Text>
                </Card.Body>
              </Col>
            </Row>
          </Card>
        ))
      ) : (
        <h4>Looks like you don't have any characters yet! Get started here!</h4>
      )}
    </Container>
  );
};
