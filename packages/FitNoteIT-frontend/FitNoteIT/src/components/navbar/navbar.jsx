import React, { Fragment } from "react";
import { Outlet } from "react-router-dom";
import SwipeableTemporaryDrawer from "../menu/menu";
import "./navbar.scss";
import { Link } from "react-router-dom";

function Navbar() {
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
            <Link to='/'>
              <button className="navbar__start-button">Zacznij trening!</button>
            </Link>
          </div>
          <div className="right">
            <Link to='/'>
              <p>stwórz plan</p>
            </Link>
            </div>
            <div className="right2">
            <Link to='/'>
              <button className="navbar__logout-button">Wyloguj</button>
            </Link>
          </div>
        </div>
      </div>
      <Outlet />
    </Fragment>
  );
}

export default Navbar;