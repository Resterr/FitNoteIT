import React from "react";
import "./plansPage.scss";
import { Footer } from "../../components/footer";
import { Plans } from "../../components/plans";

export const PlansPage: React.FC = () => {
  return (
    <div className="plans-page">
      <Plans />
      <Footer />
    </div>
  );
};
