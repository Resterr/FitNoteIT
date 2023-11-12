import React, { useEffect } from "react";
import "./workoutHistory.scss";
import { Footer } from "../../components/footer";
import workoutMan from "../../utils/workoutman.png";
import { WorkoutHistoryPanel } from "../../components/workoutHistoryPanel";
import { useNavigate, useParams } from "react-router-dom";

export const WorkoutHistory: React.FC = () => {
  const navigate = useNavigate();
  const currentUser = localStorage.getItem("currentUser");
  useEffect(() => {
    if (!currentUser) {
      navigate("/");
    }
  }, [currentUser, navigate]);
  const { id } = useParams();
  return (
    <div className="workout-history-page">
      <div className="workout-history-page-content">
        <img
          className="workout-history-page-content__photo"
          src={workoutMan}
          alt="gym"
        />
        {id && <WorkoutHistoryPanel id={id} />}
      </div>
      <Footer />
    </div>
  );
};
