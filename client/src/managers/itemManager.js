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
