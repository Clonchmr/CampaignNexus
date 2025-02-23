//Removes an image from cloudinary.
//Expects an object with a publicId key and string value
export const deleteImage = async (publicIdObj) => {
  const response = await fetch("/api/cloudinary/delete-image", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(publicIdObj),
  });

  if (!response.ok) {
    throw new Error(`HTTP error! Status: ${response.status}`);
  }
};
