import { useEffect, useState } from "react";
import { Button } from "react-bootstrap";

export const CloudinaryUploadWidget = ({ setUploadedImage, darkMode }) => {
  const [cloudName, setCloudName] = useState("");

  const uploadPreset = "CampaignNexus";

  useEffect(() => {
    setCloudName("dxrufjdna");
  }, []);

  const openUploadWidget = () => {
    if (!cloudName) {
      console.error("Cloudinary cloudNAme is not set yet.");
      return;
    }

    const widgetWindowColor = darkMode ? "#d9d9d9" : "white";

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
            tabIcon: "black",

            link: "#6e0d25",
            action: "#fff1d0",
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
