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

//Edits an existing campaign
//Expects a campaign object with the campaigns id
export const editCampaign = async (campaignObj) => {
  const response = await fetch(`${_apiString}/${campaignObj.id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(campaignObj),
  });

  if (!response.ok) {
    throw new Error(`HTTP Error! Status: ${response.status}`);
  }
};

//Marks a campaign as completed by assigning it an EndTime of the current time
export const completeCampaign = async (id) => {
  const response = await fetch(`${_apiString}/${id}/complete`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
  });

  if (!response.ok) {
    throw new Error(`HTTP Error! Status: ${response.status}`);
  }
};

//Deletes a campaign by its id
export const deleteCampaign = async (id) => {
  const response = await fetch(`${_apiString}/${id}`, {
    method: "DELETE",
    headers: {
      "Content-Type": "application/json",
    },
  });

  if (!response.ok) {
    throw new Error(`HTTP Error! Status: ${response.status}`);
  }
};
