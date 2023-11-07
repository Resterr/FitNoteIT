import React from "react";
import "./yourPlan.scss";
import RemoveCircleOutlineIcon from "@mui/icons-material/RemoveCircleOutline";

export const YourPlan: React.FC = () => {
  return (
    <div className="add-plan-page-content__your-plan">
      <div className="add-plan-page-content__your-plan-title">
        <h1>TWÓJ PLAN</h1>{" "}
      </div>
      <div className="add-plan-page-content__your-plan-second-title">
        <span>ĆWICZENIA</span>
      </div>
      <div className="add-plan-page-content__your-plan-list">
        <ul>
          <li className="add-plan-page-content__your-plan-list-element">
            Wyciskanie
            <span className="add-plan-page-content__your-plan-list-element-minus">
              <RemoveCircleOutlineIcon />
            </span>
          </li>{" "}
          <li className="add-plan-page-content__your-plan-list-element">
            Wyciskanie
            <span className="add-plan-page-content__your-plan-list-element-minus">
              <RemoveCircleOutlineIcon />
            </span>
          </li>{" "}
          <li className="add-plan-page-content__your-plan-list-element">
            Wyciskanie
            <span className="add-plan-page-content__your-plan-list-element-minus">
              <RemoveCircleOutlineIcon />
            </span>
          </li>{" "}
          <li className="add-plan-page-content__your-plan-list-element">
            Wyciskanie
            <span className="add-plan-page-content__your-plan-list-element-minus">
              <RemoveCircleOutlineIcon />
            </span>
          </li>{" "}
          <li className="add-plan-page-content__your-plan-list-element">
            Wyciskanie
            <span className="add-plan-page-content__your-plan-list-element-minus">
              <RemoveCircleOutlineIcon />
            </span>
          </li>{" "}
          <li className="add-plan-page-content__your-plan-list-element">
            Wyciskanie
            <span className="add-plan-page-content__your-plan-list-element-minus">
              <RemoveCircleOutlineIcon />
            </span>
          </li>{" "}
          <li className="add-plan-page-content__your-plan-list-element">
            Wyciskanie
            <span className="add-plan-page-content__your-plan-list-element-minus">
              <RemoveCircleOutlineIcon />
            </span>
          </li>{" "}
          <li className="add-plan-page-content__your-plan-list-element">
            Wyciskanie
            <span className="add-plan-page-content__your-plan-list-element-minus">
              <RemoveCircleOutlineIcon />
            </span>
          </li>
        </ul>
      </div>
      <button className="add-plan-page-content__your-plan-submit">
        ZAPISZ
      </button>
    </div>
  );
};
