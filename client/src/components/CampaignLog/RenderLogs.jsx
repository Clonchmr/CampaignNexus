import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Button, Card, Col, Modal, Row } from "react-bootstrap";
import { Form } from "react-bootstrap";
import {
  deleteLog,
  getLogById,
  updateLog,
} from "../../managers/campaignLogManager";
import { useEffect, useState } from "react";

export const RenderLogs = ({
  loggedInUser,
  logToEdit,
  setLogToEdit,
  logToggle,
  campaign,
  darkMode,
  fetchCampaign,
}) => {
  const [editedLog, setEditedLog] = useState({
    id: 0,
    campaignId: 0,
    title: "",
    body: "",
  });
  const [deleteLogModal, setDeleteLogModal] = useState(false);
  const [deleteModalTarget, setDeleteModalTarget] = useState(0);

  const logDeleteToggle = () => setDeleteLogModal(!deleteLogModal);

  useEffect(() => {
    getLogById(logToEdit).then(setEditedLog);
  }, [logToEdit]);
  const handleInputChange = (e) => {
    const { name, value } = e.target;

    setEditedLog((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleUpdateLog = (e) => {
    e.preventDefault();

    const newLogInfo = {
      id: editedLog.id,
      title: editedLog.title,
      body: editedLog.body,
    };

    updateLog(newLogInfo).then(() => {
      setLogToEdit(null);
      fetchCampaign(campaign.id);
    });
  };

  const handleDeleteLog = (logId) => {
    deleteLog(logId).then(() => fetchCampaign(campaign.id));
    logDeleteToggle();
  };
  return (
    <Row
      className="campaignDetails-logs-container mt-5"
      style={{ width: "75%", margin: "auto" }}
    >
      <Col md={10}>
        <h4 id="campaign-logs-h4">Campaign Logs</h4>
      </Col>
      <Col md={2}>
        <Button className="btn-primary" onClick={logToggle}>
          Add Log
        </Button>
      </Col>
      {campaign.campaignLogs?.length > 0 ? (
        campaign.campaignLogs?.map((log) => (
          <Card key={log.id} className="card-background mt-4">
            <Card.Header className="primaryText-color-bold">
              {log.id !== logToEdit ? (
                log.title
              ) : (
                <Form>
                  <Form.Group>
                    <Form.Control
                      type="text"
                      name="title"
                      value={editedLog.title}
                      data-bs-theme={`${darkMode ? "dark" : "light"}`}
                      onChange={(e) => handleInputChange(e)}
                    />
                  </Form.Group>
                </Form>
              )}
            </Card.Header>
            <Card.Body>
              <Card.Text className="primaryText-color">
                {log.id !== logToEdit ? (
                  log.body?.length > 100 ? (
                    `${log.body?.slice(0, 100)}...`
                  ) : (
                    log.body
                  )
                ) : (
                  <Form>
                    <Form.Group>
                      <Form.Control
                        as="textarea"
                        name="body"
                        value={editedLog.body}
                        data-bs-theme={darkMode ? "dark" : "light"}
                        onChange={(e) => handleInputChange(e)}
                      />
                    </Form.Group>
                  </Form>
                )}
              </Card.Text>
            </Card.Body>
            <Card.Footer>
              <Row>
                {(campaign.ownerId === loggedInUser.id ||
                  campaign.characters.some(
                    (c) => c.userId === loggedInUser.id
                  )) && (
                  <>
                    <Col xs="auto">
                      <Button
                        onClick={
                          logToEdit !== log.id
                            ? () => setLogToEdit(log.id)
                            : (e) => handleUpdateLog(e)
                        }
                      >
                        {logToEdit !== log.id ? (
                          <FontAwesomeIcon icon="fa-solid fa-pen-to-square" />
                        ) : (
                          <FontAwesomeIcon icon="fa-solid fa-check" />
                        )}
                      </Button>
                    </Col>
                    <Col xs="auto">
                      <Button
                        onClick={
                          logToEdit !== log.id
                            ? () => {
                                setDeleteModalTarget(log);
                                logDeleteToggle();
                              }
                            : () => {
                                setLogToEdit(null);
                                setEditedLog({});
                              }
                        }
                      >
                        {logToEdit !== log.id ? (
                          <FontAwesomeIcon icon="fa-solid fa-trash-can" />
                        ) : (
                          <FontAwesomeIcon icon="fa-solid fa-xmark" />
                        )}
                      </Button>
                    </Col>
                  </>
                )}

                <Col className="text-end primaryText-color">
                  {log.date?.split("T")[0]}
                </Col>
              </Row>
            </Card.Footer>
          </Card>
        ))
      ) : (
        <p>No Logs currently recorded for this campaign</p>
      )}
      <Modal
        show={deleteLogModal}
        onHide={logDeleteToggle}
        data-bs-theme={darkMode ? "dark" : "light"}
      >
        <Modal.Header closeButton>
          <Modal.Title>
            {`Delete the log: ${deleteModalTarget.title}?`}
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>Are you sure you want to delete this log?</Modal.Body>
        <Modal.Footer>
          <Button
            onClick={() => {
              handleDeleteLog(deleteModalTarget.id).then(() =>
                logDeleteToggle()
              );
            }}
          >
            Delete
          </Button>
          <Button onClick={logDeleteToggle}>Cancel</Button>
        </Modal.Footer>
      </Modal>
    </Row>
  );
};
