import React from "react";
import "./workoutHistory.scss";
import { Footer } from "../../components/footer";
import workoutMan from "../../utils/workoutman.png";
import { WorkoutHistoryPanel } from "../../components/workoutHistoryPanel";

export const WorkoutHistory: React.FC = () => {
  return (
    <div className="workout-history-page">
      <div className="workout-history-page-content">
        <img
          className="workout-history-page-content__photo"
          src={workoutMan}
          alt="gym"
        />
        <WorkoutHistoryPanel />
      </div>
      <Footer />
    </div>
  );
};
