import React, { useEffect } from "react";
import "./workoutPage.scss";
import { Footer } from "../../components/footer";
import { WorkoutCard } from "../../components/workoutCard";
import { useNavigate, useParams } from "react-router-dom";

export const WorkoutPage: React.FC = () => {
  const navigate = useNavigate();
  const currentUser = localStorage.getItem("currentUser");
  useEffect(() => {
    if (!currentUser) {
      navigate("/");
    }
  }, [currentUser, navigate]);
  const { id } = useParams();

  return (
    <div className="workout-page">
      {id && <WorkoutCard id={id} />}
      <Footer />
    </div>
  );
};
