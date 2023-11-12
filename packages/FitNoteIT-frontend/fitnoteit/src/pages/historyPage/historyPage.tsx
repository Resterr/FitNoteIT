import React, { useEffect } from "react";
import "./historyPage.scss";
import { Footer } from "../../components/footer";
import { History } from "../../components/history";
import { useNavigate } from "react-router-dom";

export const HistoryPage: React.FC = () => {
  const navigate = useNavigate();
  const currentUser = localStorage.getItem("currentUser");
  useEffect(() => {
    if (!currentUser) {
      navigate("/");
    }
  }, [currentUser, navigate]);
  return (
    <div className="history-page">
      <History />
      <Footer />
    </div>
  );
};
