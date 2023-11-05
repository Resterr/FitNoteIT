import React, { Fragment, useContext, useEffect, useState } from "react";
import { Outlet } from "react-router-dom";
import "./navbar.scss";
import { Link } from "react-router-dom";
import { useLocation } from "react-router-dom";
import logo from "../../utils/Logo.png";
import { UsersContext, UsersContextType } from "../../contexts/user.context";
import HamburgerMenu from "../hamburgerMenu/hamburgerMenu";
import axiosInstance from "../../utils/axiosInstance";
import AdminPanelSVG from "../../utils/adminpanel.svg";
import { AxiosResponse } from "axios";
type RoleType = { name: string }[];
export const Navbar: React.FC = () => {
  const { currentUserFromContext, setCurrentUserFromContext } = useContext(
    UsersContext,
  ) as UsersContextType;
  const currentUser = localStorage.getItem("currentUser");
  const [adminPanelVisible, SetAdminPanelVisible] = useState<boolean>(false);

  useEffect(() => {
    if (!currentUser) {
      return;
    }
    setCurrentUserFromContext(currentUser);
    checkRole();
  }, []);

  useEffect(() => {
    checkRole();
  }, [currentUser, currentUserFromContext]);

  const checkRole = async () => {
    let token: string | null = localStorage.getItem("accessToken");
    let config2: { headers: { Authorization: string } } = {
      headers: { Authorization: `Bearer ${token}` },
    };

    await axiosInstance
      .get("/api/users/current", config2)
      .then((response: AxiosResponse<any, any>): void => {
        if (response.status == 200) {
          let roles: RoleType = response.data.roles;
          if (
            roles.some(
              (role: { name: string }): boolean => role.name === "Admin",
            )
          ) {
            SetAdminPanelVisible(true);
          }
        } else {
          console.log("blad sprawdzania usera");
        }
      })
      .catch((e) => {
        console.log(e);
      });
  };

  const location = useLocation();

  const logoutHandler = () => {
    SetAdminPanelVisible(false);
    localStorage.setItem("currentUser", "");
    localStorage.setItem("accessToken", "");
    localStorage.setItem("refreshToken", "");
    setCurrentUserFromContext(undefined);
  };

  return (
    <Fragment>
      {adminPanelVisible === true ? (
        <div className={"AdminPanel"}>
          <Link to="/admin">
            <img
              className={"AdminPanel__img"}
              alt={"adminPanelMenu"}
              src={AdminPanelSVG}
            ></img>{" "}
          </Link>
        </div>
      ) : null}

      <HamburgerMenu />
      <div className="navbar">
        <div className="navbar__top"></div>
        <div className="navbar__bottom">
          <div className="left">
            {currentUser ? (
              <Link to="/records">
                <p>Rekordy</p>
              </Link>
            ) : (
              <p></p>
            )}
          </div>
          <div className="left2">
            {currentUser ? (
              <Link to="/">
                <p>Historia</p>
              </Link>
            ) : (
              <p></p>
            )}
          </div>
          <div className="center">
            {location.pathname === "/" ? (
              currentUser ? (
                <Link to="/">
                  <button className="navbar__start-button">
                    Zacznij trening!
                  </button>
                </Link>
              ) : (
                <p></p>
              )
            ) : (
              <Link to="/">
                <img className="navabar__logo" src={logo} alt="logo" />
              </Link>
            )}
          </div>
          <div className="right">
            {currentUser ? (
              <Link to="/">
                <p>stwórz plan</p>
              </Link>
            ) : (
              <p></p>
            )}
          </div>
          <div className="right2">
            {currentUser ? (
              <button className="navbar__logout-button" onClick={logoutHandler}>
                Wyloguj{" "}
                {currentUserFromContext ? `${currentUserFromContext}` : ""}
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
          </div>
        </div>
      </div>
      <Outlet />
    </Fragment>
  );
};
