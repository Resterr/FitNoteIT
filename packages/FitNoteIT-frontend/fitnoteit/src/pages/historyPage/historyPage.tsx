import React from "react";
import "./historyPage.scss";
import { Footer } from "../../components/footer";
import { History } from "../../components/history";

export const HistoryPage: React.FC = () => {
  return (
    <div className="history-page">
      <History />
      <Footer />
    </div>
  );
};
