import React from "react";
import "./availableExercises.scss";
import NavigateNextIcon from "@mui/icons-material/NavigateNext";
import NavigateBeforeIcon from "@mui/icons-material/NavigateBefore";
import ControlPointIcon from "@mui/icons-material/ControlPoint";

export const AvailableExercises: React.FC = () => {
  return (
    <div className="add-plan-page-content__available-exercises">
      <div className="add-plan-page-content__available-exercises-title">
        <h1>DOSTĘPNE ĆWICZENIA</h1>
      </div>
      <div className="add-plan-page-content__available-exercises-category">
        <span className="add-plan-page-content__available-exercises-category-before">
          <NavigateBeforeIcon />
        </span>
        <span>KLATA</span>
        <span className="add-plan-page-content__available-exercises-category-next">
          <NavigateNextIcon />
        </span>
      </div>
      <div className="add-plan-page-content__available-exercises-list">
        <ul>
          <li className="add-plan-page-content__available-exercises-list-element">
            Wyciskanie
            <span className="add-plan-page-content__available-exercises-list-element-plus">
              <ControlPointIcon />
            </span>
          </li>
          <li className="add-plan-page-content__available-exercises-list-element">
            Wyciskanie
            <span className="add-plan-page-content__available-exercises-list-element-plus">
              <ControlPointIcon />
            </span>
          </li>
          <li className="add-plan-page-content__available-exercises-list-element">
            Wyciskanie
            <span className="add-plan-page-content__available-exercises-list-element-plus">
              <ControlPointIcon />
            </span>
          </li>
          <li className="add-plan-page-content__available-exercises-list-element">
            Wyciskanie
            <span className="add-plan-page-content__available-exercises-list-element-plus">
              <ControlPointIcon />
            </span>
          </li>
          <li className="add-plan-page-content__available-exercises-list-element">
            Wyciskanie
            <span className="add-plan-page-content__available-exercises-list-element-plus">
              <ControlPointIcon />
            </span>
          </li>{" "}
          <li className="add-plan-page-content__available-exercises-list-element">
            Wyciskanie
            <span className="add-plan-page-content__available-exercises-list-element-plus">
              <ControlPointIcon />
            </span>
          </li>{" "}
          <li className="add-plan-page-content__available-exercises-list-element">
            Wyciskanie
            <span className="add-plan-page-content__available-exercises-list-element-plus">
              <ControlPointIcon />
            </span>
          </li>
        </ul>
      </div>
    </div>
  );
};
