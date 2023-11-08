import React, { useEffect } from "react";
import "./plansPage.scss";
import { Footer } from "../../components/footer";
import { Plans } from "../../components/plans";
import { useNavigate } from "react-router-dom";

export const PlansPage: React.FC = () => {
  const currentUser = localStorage.getItem("currentUser");
  const navigate = useNavigate();
  useEffect(() => {
    if (!currentUser) {
      navigate("/");
    }
  }, [currentUser, navigate]);
  return (
    <div className="plans-page">
      <Plans />
      <Footer />
    </div>
  );
};
