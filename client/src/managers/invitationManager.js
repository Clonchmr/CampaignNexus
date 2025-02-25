const _apiString = "/api/invite";

//Gets all pending invitations to a campaign
//Expects the id of a campaign
export const getPendingCampaignInvites = async (id) => {
  const response = await fetch(`${_apiString}/campaign/${id}`);

  if (response === 404) {
    return response.text();
  }

  if (!response.ok) {
    throw new Error(`HTTP Error! Status: ${response.status}`);
  }

  return response.json();
};

//Creates a new invitation
//Expects an object with senderId, recipientId, and campaignId
export const sendInvite = async (inviteObj) => {
  const response = await fetch(_apiString, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(inviteObj),
  });

  if (response.status === 404) {
    return response.text();
  }
  if (!response.ok) {
    throw new Error(`HTTP Error! Status: ${response.status}`);
  }

  return response.json();
};

//Deletes an invitation
//Expects an invitation id
export const deleteInvitation = async (id) => {
  const response = await fetch(`${_apiString}/${id}`, {
    method: "DELETE",
    headers: {
      "Content-Type": "application/json",
    },
  });

  if (response === 404) {
    return response.text();
  }

  if (!response.ok) {
    throw new Error(`HTTP Error! Status: ${response.status}`);
  }
};
