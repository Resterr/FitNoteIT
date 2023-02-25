import React, { Fragment } from "react";
import { Outlet} from "react-router-dom";
import "./navbar.scss";

function Navbar() {
  return (
    <Fragment>
     <h1>navbar</h1>
     <Outlet/>
    </Fragment>
  );
}

export default Navbar;