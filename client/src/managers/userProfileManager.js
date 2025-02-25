const _apiString = "/api/UserProfile";

//Gets all User Profiles
export const getAllUserProfiles = async () => {
  const response = await fetch(_apiString);

  if (!response.ok) {
    throw new Error(`HTTP Error! Status: ${response.status}`);
  }

  return response.json();
};
