import React from "react";
import "./workoutHistoryTable.scss";
import CloseIcon from "@mui/icons-material/Close";

interface WorkoutHistoryTableProps {
  textButton: string;
}
export const WorkoutHistoryTable: React.FC<WorkoutHistoryTableProps> = ({
  textButton,
}) => {
  return (
    <div className="workout-history-page-content__panel-table">
      <div className="workout-history-page-content__panel-table-name">
        PRZYSIAD
      </div>
      <div className="workout-history-page-content__panel-table-legend">
        <span>POWTÓRZENIA</span>
        <CloseIcon />
        <span>CIĘŻAR KG</span>
      </div>
      <div className="workout-history-page-content__panel-table-list">
        <ul>
          <li className="workout-history-page-content__panel-table-list-element">
            <span>10</span>
            <span>20</span>
          </li>{" "}
          <li className="workout-history-page-content__panel-table-list-element">
            <span>10</span>
            <span>20</span>
          </li>{" "}
          <li className="workout-history-page-content__panel-table-list-element">
            <span>10</span>
            <span>20</span>
          </li>{" "}
          <li className="workout-history-page-content__panel-table-list-element">
            <span>10</span>
            <span>20</span>
          </li>{" "}
          <li className="workout-history-page-content__panel-table-list-element">
            <span>10</span>
            <span>20</span>
          </li>{" "}
          <li className="workout-history-page-content__panel-table-list-element">
            <span>10</span>
            <span>20</span>
          </li>{" "}
          <li className="workout-history-page-content__panel-table-list-element">
            <span>10</span>
            <span>20</span>
          </li>
        </ul>
      </div>
      <div className="workout-history-page-content__panel-table-button">
        <button type="button">{textButton}</button>
      </div>
    </div>
  );
};
