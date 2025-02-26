const _apiString = "/api/invite";

//Gets all pending invitations to a campaign or to a user
//Include a recipientId if you want pending invites to a user
//Include a campaignId if you want pending invites to a campaign
export const getPendingInvites = async (
  recipientId = null,
  campaignId = null
) => {
  const params = new URLSearchParams();

  if (recipientId !== null) {
    params.append("recipientId", recipientId);
  }

  if (campaignId !== null) {
    params.append("campaignId", campaignId);
  }

  const url = params.toString()
    ? `${_apiString}/pending?${params.toString()}`
    : `${_apiString}/pending`;

  const response = await fetch(url);

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

//Accepts an invite to a campaign by creating a new characterCampaign entity
//Expects an object with campaignId and characterId
export const acceptInvite = async (characterCampaignObj) => {
  const response = await fetch(`${_apiString}/accept`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(characterCampaignObj),
  });

  if (response === 404) {
    return response.text();
  }

  if (!response.ok) {
    throw new Error(`HTTP Error! Status: ${response.status}`);
  }

  return response.json();
};
