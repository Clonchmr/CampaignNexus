const _apiString = "/api/campaignlog";

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
