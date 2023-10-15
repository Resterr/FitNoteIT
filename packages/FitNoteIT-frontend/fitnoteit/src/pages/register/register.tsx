import React from "react";
import "./register.scss";
import mobilephoneWoman from "../../assets/mobilephoneWoman.png";
import { Footer } from "../../components/footer";
import { RegisterForm } from "./form/registerForm";

export const Register: React.FC = () => {
  return (
    <div>
      <div className="register">
        <RegisterForm />
        <div className="register__img">
          <img
            className="register__img-photo"
            src={mobilephoneWoman}
            alt="woman"
          />
        </div>
      </div>

      <Footer />
    </div>
  );
};
