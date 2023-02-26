import React, { Fragment } from "react";
import { Outlet } from "react-router-dom";
import SwipeableTemporaryDrawer from "../menu/menu";
import "./navbar.scss";
import { Link } from "react-router-dom";
import { useLocation } from 'react-router-dom';
import logo from "../../assets/Logo.png"

function Navbar() {
  const location = useLocation();

  return (
    <Fragment>
      <div className="navbar">
        <div className="navbar__top">

        </div>
        <div className="navbar__bottom">
          <div className="left">
            <Link to='/'>
              <p>Rekordy</p>
            </Link>
          </div>
          <div className="left2">
            <Link to='/'>
              <p>Historia</p>
            </Link>
          </div>
          <div className="center">
            {location.pathname === '/' ? (
              <Link to='/'>
              <button className="navbar__start-button">Zacznij trening!</button>
            </Link>
            ) : (
              <Link to='/'>
              <img className="navabar__logo" src={logo} alt="logo"/>
            </Link>
            )}
            
          </div>
          <div className="right">
            <Link to='/'>
              <p>stwórz plan</p>
            </Link>
          </div>
          <div className="right2">
          {location.pathname === '/' ? (
            <Link to='/login'>
              <button className="navbar__logout-button">Zaloguj</button>
            </Link>
            ) : location.pathname === '/login' ? (
              <Link to='/register'>
              <button className="navbar__logout-button">Zarejestruj</button>
            </Link>
            ) : (
              <Link to='/login'>
              <button className="navbar__logout-button">Zaloguj</button>
            </Link>
            )}
           
          </div>
        </div>
      </div>
      <Outlet />
    </Fragment>
  );
}

export default Navbar;