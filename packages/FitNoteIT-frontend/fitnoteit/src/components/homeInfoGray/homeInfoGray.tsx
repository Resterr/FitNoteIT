import React, { useState } from "react";
import "./homeInfoGray.scss";
import { ButtonInfoHome } from "../buttonInfoHome";
import { Link } from "react-router-dom";

interface HomeInfoGrayProps {
  title: string;
  text: string;
  buttonText: string;
  toLink: string;
}

export const HomeInfoGray: React.FC<HomeInfoGrayProps> = ({
  title,
  text,
  buttonText,
  toLink,
}) => {
  const currentUser = localStorage.getItem("currentUser");
  const [toLinkState, setToLinkState] = useState<string>(toLink);

  return (
    <div className="home__info">
      <h2>{title}</h2>
      <div className="home__info-text">{text}</div>
      <Link to={`/${toLinkState}`}>
        {currentUser && <ButtonInfoHome textButton={buttonText} />}{" "}
      </Link>
    </div>
  );
};
