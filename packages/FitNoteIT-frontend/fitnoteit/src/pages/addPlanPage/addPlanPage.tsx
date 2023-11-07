import React from "react";
import "./addPlanPage.scss";
import { Footer } from "../../components/footer";
import { AvailableExercises } from "../../components/availableExercises";
import plansGym from "../../utils/plansgym.png";
import { YourPlan } from "../../components/yourPlan";

export const AddPlanPage: React.FC = () => {
  return (
    <div className="add-plan-page">
      <div className="add-plan-page-content">
        <AvailableExercises />
        <img className="add-plan-page-photo" src={plansGym} alt="gym" />
        <YourPlan />
      </div>
      <Footer />
    </div>
  );
};
