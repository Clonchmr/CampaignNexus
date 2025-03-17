const _apiString = "/api/characteritem";

//Uses a charge of a consumable, and decreases its quantity by 1
//If the quantity is already at 1, it deletes it instead
//Expects the Id of the character item entity, and expects the item to be a consumable
export const useConsumable = async (characterItemId) => {
  const response = await fetch(`${_apiString}/${characterItemId}`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
  });

  if (response == 404 || response === 400) {
    return response.text();
  }

  if (!response.ok) {
    throw new Error(`HTTP Error! Status: ${response.status}`);
  }
};
