const _apiString = "/api/species";

export const getAllSpecies = async () => {
  const response = await fetch(_apiString);

  if (!response.ok) {
    throw new Error(`HTTP Error! Status: ${response.status}`);
  }

  return response.json();
};
