import React, { Fragment, useContext, useEffect } from "react";
import { Outlet } from "react-router-dom";
import styles from "./HamburgerMenu.module.scss";
import { UsersContext, UsersContextType } from "../../contexts/user.context";
import { useLocation } from "react-router-dom";
import { Link } from "react-router-dom";

type MenuProps = {
  className?: string;
};

type MenuState = {
  currentUser: string | null;
};

export const Menu: React.FC<MenuProps> = ({ className }) => {
  const { currentUser2, setCurrentUser2 } = useContext(
    UsersContext
  ) as UsersContextType;

  const currentUser: string | null = localStorage.getItem("currentUser");

  useEffect(() => {
    if (currentUser) {
      setCurrentUser2(currentUser);
    } else {
      setCurrentUser2(undefined);
    }
  }, []);

  const location = useLocation();

  const logoutHandler = () => {
    localStorage.setItem("currentUser", "");
    localStorage.setItem("accessToken", "");
    localStorage.setItem("refreshToken", "");

    setCurrentUser2("");
  };

  return (
    <div className={className}>
      <ul className={styles.navigation__list}>
        <li className={styles.navigation__item}>
          {currentUser ? (
            <button className="navbar__logout-button" onClick={logoutHandler}>
              Wyloguj {currentUser2 ? `${currentUser2}` : ""}
            </button>
          ) : location.pathname === "/" ? (
            <Link to="/login">
              <button className="navbar__logout-button">Zaloguj</button>
            </Link>
          ) : location.pathname === "/login" ? (
            <Link to="/register">
              <button className="navbar__logout-button">Zarejestruj</button>
            </Link>
          ) : (
            <Link to="/login">
              <button className="navbar__logout-button">Zaloguj</button>
            </Link>
          )}
        </li>
        <li className={styles.navigation__item}>
          {currentUser ? (
            <Link to="/">
              <p>Historia</p>
            </Link>
          ) : (
            <p></p>
          )}
        </li>
        <li className={styles.navigation__item}>
          {currentUser ? (
            <Link to="/">
              <p>stwórz plan</p>
            </Link>
          ) : (
            <p></p>
          )}
        </li>
        <li className={styles.navigation__item}>
          {currentUser ? (
            <Link to="/records">
              <p>Rekordy</p>
            </Link>
          ) : (
            <p></p>
          )}
        </li>
      </ul>
    </div>
  );
};
