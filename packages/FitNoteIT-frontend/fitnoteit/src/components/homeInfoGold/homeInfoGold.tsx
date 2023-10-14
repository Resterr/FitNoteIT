import React from "react";
import "./homeInfoGold.scss";
import woman from "../../utils/woman.png";

interface HomeInfoGrayProps {
  text: string;
}

export const HomeInfoGold: React.FC<HomeInfoGrayProps> = ({ text }) => {
  return (
    <div className="home__gold-element">
      <div className="home__gold-element-text">{text}</div>
      <div className="home__gold-element-img">
        <img className="home__img" src={woman} alt="woman" />
      </div>
    </div>
  );
};
