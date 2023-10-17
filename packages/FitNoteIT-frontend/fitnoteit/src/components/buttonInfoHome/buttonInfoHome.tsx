import React from "react";

interface ButtonInfoHomeProps {
  textButton: string;
}
export const ButtonInfoHome: React.FC<ButtonInfoHomeProps> = ({
  textButton,
}) => {
  return <button className="home__info-button">{textButton}</button>;
};
