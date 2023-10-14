import React from "react";
import "./homeInfoGray.scss";
import { ButtonInfoHome } from "../buttonInfoHome";

interface HomeInfoGrayProps {
  title: string;
  text: string;
  buttonText: string;
}

export const HomeInfoGray: React.FC<HomeInfoGrayProps> = ({
  title,
  text,
  buttonText,
}) => {
  return (
    <div className="home__info">
      <h2>{title}</h2>
      <div className="home__info-text">{text}</div>
      <ButtonInfoHome textButton={buttonText} />
    </div>
  );
};
