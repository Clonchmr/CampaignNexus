const _apiString = "/api/alignment";

//Gets all alignments
export const getAllAlignments = async () => {
  const response = await fetch(_apiString);

  if (!response.ok) {
    throw new Error(`HTTP Error! Status: ${response.status}`);
  }

  return response.json();
};
