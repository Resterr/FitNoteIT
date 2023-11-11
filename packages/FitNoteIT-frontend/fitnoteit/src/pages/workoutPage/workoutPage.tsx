import React from "react";
import "./workoutPage.scss";
import { Footer } from "../../components/footer";
import { WorkoutCard } from "../../components/workoutCard";
import { useParams } from "react-router-dom";

export const WorkoutPage: React.FC = () => {
  const { id } = useParams();

  return (
    <div className="workout-page">
      {id && <WorkoutCard id={id} />}
      <Footer />
    </div>
  );
};
