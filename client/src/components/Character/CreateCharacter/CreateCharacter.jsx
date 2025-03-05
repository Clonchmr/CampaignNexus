import { useEffect, useState } from "react";
import { Container } from "react-bootstrap";

import { getAllClasses } from "../../../managers/classManager";
import { CreateStepFour } from "./CreateStepFour";
import { CreateStepThree } from "./CreateStepThree";
import { CreateStepTwo } from "./CreateStepTwo";
import { CreateStepOne } from "./CreateStepOne";
import { getAllAlignments } from "../../../managers/alignmentManager";
import { getAllSpecies } from "../../../managers/speciesManager";
import { createCharacter } from "../../../managers/characterManager";
import { useNavigate } from "react-router-dom";

export const CreateCharacter = ({ loggedInUser }) => {
  const [step, setStep] = useState(1);
  const [character, setCharacter] = useState({});
  const [classes, setClasses] = useState([]);
  const [chosenClass, setChosenClass] = useState({});
  const [alignments, setAlignments] = useState([]);
  const [species, setSpecies] = useState([]);
  const [buttonIsDisabled, setButtonIsDisabled] = useState(true);
  const [objectLength, setObjectLength] = useState(6);
  const [uploadedImage, setUploadedImage] = useState(null);

  const navigate = useNavigate();

  useEffect(() => {
    const characterData = Object.values(character);

    if (characterData.includes("") || characterData.length < objectLength) {
      setButtonIsDisabled(true);
    } else {
      setButtonIsDisabled(false);
    }
  }, [character, objectLength]);

  useEffect(() => {
    getAllClasses().then(setClasses);
    getAllAlignments().then(setAlignments);
    getAllSpecies().then(setSpecies);
  }, []);

  const handleInputChange = (e) => {
    const { name, value } = e.target;

    setCharacter((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleClassChoice = (e) => {
    const { name, value } = e.target;

    const foundClass = classes.find((c) => c.id === parseInt(e.target.value));

    setChosenClass(foundClass);
    setCharacter((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleCreateCharacter = () => {
    const characterHeight = character.feet + "'" + character.inches;

    const characterObj = {
      userId: loggedInUser.id,
      name: character.name,
      height: characterHeight,
      weight: character.weight,
      gender: character.gender,
      age: character.age,
      faith: character.faith,
      speciesId: parseInt(character.speciesId),
      classId: parseInt(chosenClass.id),
      strength: parseInt(character.strength),
      dexterity: parseInt(character.dexterity),
      constitution: parseInt(character.constitution),
      wisdom: parseInt(character.wisdom),
      intelligence: parseInt(character.intelligence),
      charisma: parseInt(character.charisma),
      alignmentId: parseInt(character.alignmentId),
      backstory: character.backstory,
      characterPicUrl: uploadedImage,
      characterAbilities: [
        { abilityId: character.abilityOneId },
        { abilityId: character.abilityTwoId },
      ],
    };

    createCharacter(characterObj).then((res) => {
      navigate(`/characters/${res.id}`);
    });
  };

  return (
    <Container style={{ width: "30rem" }}>
      {step === 1 && (
        <CreateStepOne
          handleInputChange={handleInputChange}
          character={character}
          buttonIsDisabled={buttonIsDisabled}
          setStep={setStep}
          setObjectLength={setObjectLength}
        />
      )}
      {step === 2 && (
        <CreateStepTwo
          character={character}
          handleClassChoice={handleClassChoice}
          handleInputChange={handleInputChange}
          classes={classes}
          chosenClass={chosenClass}
          buttonIsDisabled={buttonIsDisabled}
          setStep={setStep}
          setObjectLength={setObjectLength}
          species={species}
        />
      )}
      {step === 3 && (
        <CreateStepThree
          character={character}
          handleInputChange={handleInputChange}
          setStep={setStep}
          setObjectLength={setObjectLength}
          buttonIsDisabled={buttonIsDisabled}
        />
      )}
      {step === 4 && (
        <CreateStepFour
          character={character}
          alignments={alignments}
          buttonIsDisabled={buttonIsDisabled}
          setStep={setStep}
          setObjectLength={setObjectLength}
          handleInputChange={handleInputChange}
          uploadedImage={uploadedImage}
          setUploadedImage={setUploadedImage}
          handleCreateCharacter={handleCreateCharacter}
        />
      )}
    </Container>
  );
};
