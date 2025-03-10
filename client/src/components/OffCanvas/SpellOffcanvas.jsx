import { useContext } from "react";
import { Offcanvas } from "react-bootstrap";
import { ThemeContext } from "../../ThemeContext/ThemeContext";

export const SpellOffcanvas = ({
  spellTarget,
  spellOffcanvas,
  spellOffCanvasToggle,
}) => {
  const { darkMode } = useContext(ThemeContext);
  return (
    <Offcanvas
      show={spellOffcanvas}
      onHide={spellOffCanvasToggle}
      placement="end"
      data-bs-theme={darkMode ? "dark" : "light"}
    >
      <Offcanvas.Header closeButton></Offcanvas.Header>
      <Offcanvas.Body className="text-center">
        {!Object.keys(spellTarget)?.includes("itemId") ? (
          <>
            <h6>{spellTarget.ability?.abilityName}</h6>
            <p>
              <small>Casting Time: </small>
              {spellTarget.ability?.castingTime}
            </p>
            <p>
              <small>Range: </small>
              {spellTarget.ability?.range}
            </p>
            {!spellTarget.ability?.abilityType?.includes("Action") && (
              <p>
                <small>Components: </small>
                {spellTarget.ability?.notes}
              </p>
            )}
            <p>{spellTarget.ability?.abilityDescription}</p>{" "}
          </>
        ) : (
          <>
            <h6>{spellTarget.item?.itemName}</h6>

            {spellTarget.item?.damage ? (
              <p>
                <small>Damage: </small>
                {spellTarget.item?.damage}
              </p>
            ) : (
              <p>
                <small>Armor Class: </small>
                {spellTarget.item?.armorClass}
              </p>
            )}
            {spellTarget.item?.notes && (
              <p>
                <small>Notes: </small>
                {spellTarget.item?.notes}
              </p>
            )}
            <p>
              <small>Weight: </small>
              {spellTarget.item?.weight}
            </p>
            <p>{spellTarget.item?.itemDescription}</p>
          </>
        )}
      </Offcanvas.Body>
    </Offcanvas>
  );
};
