import React from "react";
import "./workoutPage.scss";
import { Footer } from "../../components/footer";
import { WorkoutCard } from "../../components/workoutCard";

export const WorkoutPage: React.FC = () => {
  return (
    <div className="workout-page">
      <WorkoutCard />
      <Footer />
    </div>
  );
};
