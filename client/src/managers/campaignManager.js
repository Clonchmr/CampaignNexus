const _apiString = "/api/Campaign";

//Get all Campaigns owned by a user. Expects a userId
//Can include an optional count parameter to limit how many campaigns you receive
//Can also include an optional showActive parameter to get campaigns with or without an EndDate. It expects a boolean.
//Campaigns are ordered by startDate in descending order
export const getCampaignsByUser = async (
  userId,
  count = null,
  showActive = null
) => {
  const params = new URLSearchParams();

  if (count !== null && count > 0) {
    params.append("count", count);
  }

  if (showActive !== null) {
    showActive
      ? params.append("showActive", true)
      : params.append("showActive", false);
  }

  const url = params.toString()
    ? `${_apiString}/user/${userId}?${params.toString()}`
    : `${_apiString}/user/${userId}`;

  const response = await fetch(url);

  if (!response.ok) {
    throw new Error(`HTTP Error! Status ${response.status}`);
  }

  return response.json();
};

//Gets a single campaign by its Id
export const getCampaignById = async (id) => {
  const response = await fetch(`${_apiString}/${id}`);

  if (!response.ok) {
    throw new Error(`HTTP Error! Status: ${response.status}`);
  }

  return response.json();
};

//Creates a new campaign.
//Expects an object with ownerId, campaignName, levelRange, campaignDescription, and an optional campaignPicUrl
export const postNewCampaign = async (campaignObj) => {
  const response = await fetch(_apiString, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(campaignObj),
  });

  if (!response.ok) {
    throw new Error(`HTTP Error! Status: ${response.status}`);
  }

  return response.json();
};
