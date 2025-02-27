const _apiString = "/api/charactercampaign";

//Removes a character from a campaign
//Expects the id of the campaign and the id of the character
export const removeCharacterFromCampaign = async (characterId, campaignId) => {
  const response = await fetch(
    `${_apiString}?characterId=${characterId}&campaignId=${campaignId}`,
    {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
      },
    }
  );

  if (response === 404) {
    return response.text();
  }

  if (!response.ok) {
    throw new Error(`HTTP Error! Status: ${response.status}`);
  }
};
