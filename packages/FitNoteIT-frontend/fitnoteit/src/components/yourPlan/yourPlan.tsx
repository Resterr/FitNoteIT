import React from "react";
import "./yourPlan.scss";
import RemoveCircleOutlineIcon from "@mui/icons-material/RemoveCircleOutline";
import { ModalYourPlan } from "./modalYourPlan";

type Exercise = {
  name: string;
  categoryName: string;
  id: number;
};

type YourPlanProps = {
  exercises: Exercise[];
  deleteHandle: (exercise: Exercise) => void;
};

export const YourPlan: React.FC<YourPlanProps> = ({
  exercises,
  deleteHandle,
}) => {
  return (
    <div className="add-plan-page-content__your-plan">
      <div className="add-plan-page-content__your-plan-title">
        <h1>TWÓJ PLAN</h1>{" "}
      </div>
      <div className="add-plan-page-content__your-plan-second-title">
        <span>ĆWICZENIA</span>
      </div>
      <div className="add-plan-page-content__your-plan-list">
        {exercises.length === 0 ? (
          <ul>
            <li className="add-plan-page-content__your-plan-list-element">
              <span className="add-plan-page-content__your-plan-list-element--center">
                Brak ćwiczeń
              </span>
            </li>
          </ul>
        ) : (
          <ul>
            {exercises.map((exercise) => (
              <li
                key={exercise.id}
                className="add-plan-page-content__your-plan-list-element"
              >
                {exercise.name}
                <span
                  className="add-plan-page-content__your-plan-list-element-minus"
                  onClick={() => deleteHandle(exercise)}
                >
                  <RemoveCircleOutlineIcon />
                </span>
              </li>
            ))}
          </ul>
        )}
      </div>
      <ModalYourPlan exercises={exercises} />
    </div>
  );
};
