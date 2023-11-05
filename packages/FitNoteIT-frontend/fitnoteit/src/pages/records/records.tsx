import React, { useEffect } from "react";
import "./records.scss";
import { Footer } from "../../components/footer";
import athlet from "../../utils/athlet.png";
import { RecordsForm } from "../../components/recordsForm";
import { useNavigate } from "react-router-dom";

export const Records: React.FC = () => {
  const navigate = useNavigate();
  const currentUser = localStorage.getItem("currentUser");
  useEffect(() => {
    if (!currentUser) {
      navigate("/");
    }
  }, [currentUser, navigate]);
  return (
    <div className="records-page">
      <div className="records">
        <div className="records__background-element"></div>
        <div className="records__img">
          <img className="records__img-photo" src={athlet} alt="athlete" />
        </div>
        {currentUser && <RecordsForm />}
      </div>

      <Footer />
    </div>
  );
};
