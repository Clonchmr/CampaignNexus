import { useState } from "react";
import { Button, Col, Container, Row } from "react-bootstrap";

export const AbilityScoreRoller = () => {
  const [rolls, setRolls] = useState([]);
  const [rollTotal, setRollTotal] = useState(0);

  const handleDiceRoll = () => {
    let rollArray = [];
    for (let i = 1; i < 5; i++) {
      let diceRoll = Math.floor(Math.random() * (7 - 1) + 1);
      rollArray.push(diceRoll);
    }
    const sortedArray = rollArray.toSorted((a, b) => b - a);
    setRolls(sortedArray);
    let totalRoll = 0;
    for (let i = 0; i < 3; i++) {
      totalRoll += sortedArray[i];
    }
    setRollTotal(totalRoll);
  };
  return (
    <Container>
      <h6 style={{ textShadow: "0 0 0.3rem black" }}>
        Roll from home, or use our dice roller!
      </h6>
      <Button className="btn-primary" onClick={handleDiceRoll}>
        Roll!
      </Button>
      <Row>
        {rolls.length > 0 &&
          rolls.map((r, index) => (
            <Col key={index}>
              <p
                className={`abilityScoreRollNumber ${
                  index === rolls.length - 1 ? "crossed-out" : ""
                }`}
              >
                {r}
              </p>
            </Col>
          ))}
      </Row>
      {rollTotal > 0 && <p id="rollTotal">{rollTotal}</p>}
    </Container>
  );
};
