const _apiString = "/api/class";

//Gets all classes with their respective available abilities
export const getAllClasses = async () => {
  const response = await fetch(_apiString);

  if (!response.ok) {
    throw new Error(`HTTP Error! Status: ${response.status}`);
  }

  return response.json();
};
