import React from "react";
import "./records.scss";
import { Footer } from "../../components/footer";
import athlet from "../../utils/athlet.png";
import { RecordsForm } from "../../components/recordsForm";

export const Records: React.FC = () => {
  return (
    <div className="records-page">
      <div className="records">
        <div className="records__background-element"></div>
        <div className="records__img">
          <img className="records__img-photo" src={athlet} alt="athlete" />
        </div>
        <RecordsForm />
      </div>

      <Footer />
    </div>
  );
};
