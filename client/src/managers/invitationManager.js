const _apiString = "/api/invite";

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
