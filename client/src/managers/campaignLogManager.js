const _apiString = "/api/campaignlog";

//Gets a CampaignLog by its id
export const getLogById = async (logId) => {
  const response = await fetch(`${_apiString}/${logId}`);

  if (!response.ok) {
    throw new Error(`HTTP Error! Status: ${response.status}`);
  }

  return response.json();
};

//Creates a new Log for a campaign
//Expects an object with campaignId, title, and body
export const newCampaignLog = async (campaignLogObj) => {
  const response = await fetch(_apiString, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(campaignLogObj),
  });
  if (!response.ok) {
    throw new Error(`HTTP Error! Status: ${response.status}`);
  }

  return response.json();
};

//Edits an existing CampaignLog
//Expects the id of the log, along with an object with title: and body:
export const updateLog = async (logObj) => {
  const response = await fetch(`${_apiString}/${logObj.id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(logObj),
  });

  if (!response.ok) {
    throw new Error(`HTTP Error! Status: ${response.status}`);
  }
};

//Deletes an existing CampaignLog
export const deleteLog = async (logId) => {
  const response = await fetch(`${_apiString}/${logId}`, {
    method: "DELETE",
    headers: {
      "Content-Type": "application/json",
    },
  });

  if (!response.ok) {
    throw new Error(`HTTP Error! Status: ${response.status}`);
  }
};
