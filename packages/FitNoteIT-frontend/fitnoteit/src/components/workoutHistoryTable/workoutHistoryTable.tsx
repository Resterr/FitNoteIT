import React from "react";
import "./workoutHistoryTable.scss";
import CloseIcon from "@mui/icons-material/Close";

interface WorkoutHistoryTableProps {
  textButton: string;
  name: string;
  array: { repeats: number; weight: number }[];
  myFunction: () => void;
  currentPage: number;
  maxPage: number;
}
export const WorkoutHistoryTable: React.FC<WorkoutHistoryTableProps> = ({
  textButton,
  name,
  array,
  myFunction,
  currentPage,
  maxPage,
}) => {
  return (
    <div className="workout-history-page-content__panel-table">
      <div className="workout-history-page-content__panel-table-name">
        {name}
      </div>
      <div className="workout-history-page-content__panel-table-legend">
        <span>POWTÓRZENIA</span>
        <CloseIcon />
        <span>CIĘŻAR KG</span>
      </div>
      <div className="workout-history-page-content__panel-table-list">
        <ul>
          {array.map((secondArray) => (
            <li
              className="workout-history-page-content__panel-table-list-element"
              key={array.indexOf(secondArray)}
            >
              {secondArray.repeats && <span>{secondArray.repeats}</span>}
              {secondArray.weight && <span>{secondArray.weight}</span>}
            </li>
          ))}
        </ul>
      </div>
      <div className="workout-history-page-content__panel-table-button">
        {(currentPage == 0 && textButton == "Poprzednia strona") ||
        (currentPage + 2 == maxPage && textButton == "Następna strona") ? (
          <></>
        ) : (
          <button onClick={myFunction} type="button">
            {textButton}
          </button>
        )}
      </div>
    </div>
  );
};
