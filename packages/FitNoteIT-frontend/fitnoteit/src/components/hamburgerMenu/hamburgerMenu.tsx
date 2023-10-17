//Core
import React from "react";
import styles from "./HamburgerMenu.module.scss";

//Components

import { Hamburger } from "./hamburger";
import { Menu } from "./menu";

//Main JSX
class HamburgerMenu extends React.Component {
  state = {
    isOpen: false,
  };

  handleClick = () => {
    this.setState({
      isOpen: !this.state.isOpen,
    });
  };

  render() {
    return (
      <>
        <Hamburger
          onClick={this.handleClick}
          className={
            this.state.isOpen
              ? `${styles.hamburger} ${styles.hamburgeractive}`
              : styles.hamburger
          }
        />
        <Menu
          className={
            this.state.isOpen
              ? `${styles.navigation} ${styles.navigationactive}`
              : styles.navigation
          }
        />
      </>
    );
  }
}

export default HamburgerMenu;
