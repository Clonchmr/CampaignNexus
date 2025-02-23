import { useEffect, useState } from "react";
import { Button } from "react-bootstrap";

export const CloudinaryUploadWidget = ({
  uploadedImage,
  setUploadedImage,
  darkMode,
}) => {
  const [cloudName, setCloudName] = useState("");

  const uploadPreset = "CampaignNexus";

  useEffect(() => {
    // fetch())
    //   .then((res) => res.json())
    //   .then((data) => setCloudName(data.cloudName))
    //   .catch((error) =>
    //     console.error("Error fetching Cloudinary config:", error)
    //   );
    setCloudName("dxrufjdna");
  }, []);

  const openUploadWidget = () => {
    if (!cloudName) {
      console.error("Cloudinary cloudNAme is not set yet.");
      return;
    }

    const widgetWindowColor = darkMode ? "#201d1e" : "white";

    const cloudinaryWidget = window.cloudinary.createUploadWidget(
      {
        cloudName: cloudName,
        uploadPreset: uploadPreset,
        cropping: true,
        showThumbnails: true,
        styles: {
          palette: {
            window: widgetWindowColor,
            windowBorder: "#201d1e",
            tabIcon: "#fff1d0",
            menuIcons: "#fff1d0",
            inactiveTabIcons: "#fff1d0",
            textDark: "#fff1d0",
            textLight: "#201d1e",
            link: "#6e0d25",
            action: "#fff1d0",
            inProgress: "#6e0d25",
          },
          fonts: {
            "'Cinzel Decorative', serif":
              "https://fonts.googleapis.com/css2?family=Cinzel+Decorative:wght@400;700;900",
          },
        },
      },
      (error, result) => {
        if (!error && result.event === "success") {
          console.log("Upload success:", result.info);

          setUploadedImage(result.info.secure_url);
        }
      }
    );
    cloudinaryWidget.open();
  };
  return <Button onClick={openUploadWidget}>Upload Image</Button>;
};
