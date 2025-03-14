const _apiString = "/api/item";

//Toggles the isEquipped status of a characterItem entity
//Expects the id of the characterItem join table entity
export const toggleEquipItem = async (id) => {
  const response = await fetch(`${_apiString}/toggle/${id}`, {
    method: "POST",
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

//Gets all items
export const getAllItems = async () => {
  const response = await fetch(_apiString);

  if (!response.ok) {
    throw new Error(`HTTP Error! Status: ${response.status}`);
  }

  return response.json();
};

//Adds an item to a character by creating a new characterItem entity
//Expects the id of the item being added, the id of the character the item is being added to, and the quantity
export const newCharacterItem = async (itemId, characterId, quantity) => {
  const response = await fetch(
    `${_apiString}/characterAdd/${itemId}?characterId=${characterId}&quantity=${quantity}`,
    {
      method: "POST",
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

  return response.json();
};
