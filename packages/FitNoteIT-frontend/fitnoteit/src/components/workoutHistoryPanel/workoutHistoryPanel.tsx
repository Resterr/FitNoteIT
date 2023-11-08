import React from "react";
import "./workoutHistoryPanel.scss";
import { WorkoutHistoryTable } from "../workoutHistoryTable";

export const WorkoutHistoryPanel: React.FC = () => {
  return (
    <div className="workout-history-page-content__panel">
      <div className="workout-history-page-content__panel-title">
        TWOJA HISTORIA
      </div>

      <div className="workout-history-page-content__panel-body">
        <WorkoutHistoryTable textButton={"POPRZEDNIA STRONA"} />
        <WorkoutHistoryTable textButton={"NASTEPNA STRONA"} />
      </div>
    </div>
  );
};
