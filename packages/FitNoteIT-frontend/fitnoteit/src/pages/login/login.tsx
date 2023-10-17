import React from "react";
import "./login.scss";
import mirrorMan from "../../utils/mirrorMan.png";
import { Footer } from "../../components/footer";
import { LoginForm } from "./form/loginForm";

export const Login: React.FC = () => {
  return (
    <div>
      <div className="login">
        <LoginForm />
        <div className="login__img">
          <img className="login__img-photo" src={mirrorMan} alt="bodybuilder" />
        </div>
      </div>

      <Footer />
    </div>
  );
};
