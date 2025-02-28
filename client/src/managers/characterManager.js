const _apiString = "/api/character";

//Gets all characters for a user, or for a campaign
//Include either a userId or a campaignId
export const getCharacters = async (
  userId = null,
  campaignId = null,
  count = null
) => {
  const params = new URLSearchParams();

  if (userId !== null) {
    params.append("userId", userId);
  }

  if (campaignId !== null) {
    params.append("campaignId", campaignId);
  }

  if (count !== null) {
    params.append("count", count);
  }

  const url = params.toString()
    ? `${_apiString}?${params.toString()}`
    : _apiString;

  const response = await fetch(url);

  if (response === 404) {
    return response.text();
  }

  if (!response.ok) {
    throw new Error(`HTTP Error! Status: ${response.status}`);
  }

  return response.json();
};
