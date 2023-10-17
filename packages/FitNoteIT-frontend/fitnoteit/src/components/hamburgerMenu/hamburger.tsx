import React from "react";
import styles from "./HamburgerMenu.module.scss";

interface HamburgerProps {
  className?: string;
  onClick: () => void;
}

export const Hamburger: React.FC<HamburgerProps> = ({ className, onClick }) => (
  <button onClick={onClick} className={className}>
    <span className={styles.hamburger__box}>
      <span className={styles.hamburger__inner} />
    </span>
  </button>
);
