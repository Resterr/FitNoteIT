import React, { Fragment } from "react";
import { Outlet} from "react-router-dom";
import "./navbar.scss";

function Navbar() {
  return (
    <Fragment>
      <div className="navbar">
        <h1>navbar</h1>
      </div>
     <Outlet/>
    </Fragment>
  );
}

export default Navbar;