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

//Gets a character by its id
export const getCharacterById = async (characterId) => {
  const response = await fetch(`${_apiString}/${characterId}`);

  if (response === 404) {
    return response.text();
  }

  if (!response.ok) {
    throw new Error(`HTTP Error! Status: ${response.status}`);
  }

  return response.json();
};

//Creates a new character
//Expects a character object
export const createCharacter = async (characterObj) => {
  const response = await fetch(_apiString, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(characterObj),
  });

  if (response === 404) {
    return response.text();
  }
  if (!response.ok) {
    throw new Error(`HTTP Error! Status: ${response.status}`);
  }

  return response.json();
};

//Levels up a character
//Expects an object with each ability score, level, and hitPoints
export const levelUpCharacter = async (levelUpObject) => {
  const response = await fetch(`${_apiString}/level/${levelUpObject.id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(levelUpObject),
  });

  if (response === 404) {
    return response.text();
  }

  if (!response.ok) {
    throw new Error(`HTTP Error! Status: ${response.status}`);
  }
};

//Updates a characters information
//Expects an object with name, height, weight, gender, faith, backstory, alignmentId, and characterPicUrl
export const updateCharacter = async (characterObj) => {
  const response = await fetch(`${_apiString}/update/${characterObj.id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(characterObj),
  });

  if (response === 404) {
    return response.text();
  }

  if (!response.ok) {
    throw new Error(`HTTP Error! Status ${response.status}`);
  }
};
