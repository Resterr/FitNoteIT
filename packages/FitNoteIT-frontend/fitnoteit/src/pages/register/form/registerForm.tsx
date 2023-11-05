import React, { useState, useEffect } from "react";
import "./registerForm.scss";
import { useForm, SubmitHandler } from "react-hook-form";
import axios, { AxiosResponse } from "axios";
import { useNavigate } from "react-router-dom";
import axiosInstance from "../../../utils/axiosInstance";

interface FormData {
  email: string;
  userName: string;
  password: string;
  confirmPassword: string;
}

export const RegisterForm: React.FC = () => {
  const currentUser = localStorage.getItem("currentUser");
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<FormData>();
  const [status, setStatus] = useState<string | undefined>();
  const navigate = useNavigate();

  useEffect(() => {
    if (currentUser) {
      navigate("/");
    }
  }, [currentUser, navigate]);

  axios.interceptors.response.use(
    (response: AxiosResponse<any, any>) => response,
    (error) => {
      if (error.response) {
        switch (error.response.status) {
          case 400:
            console.log("Nieprawidłowe żądanie");
            setStatus("Nie udało się zarejestrować");
            break;
          case 404:
            console.log("Nie znaleziono zasobu");
            setStatus("Nie udało się zarejestrować");
            break;
          default:
            console.log(`Wystąpił błąd: ${error.response.status}`);
        }
      } else {
        console.log("Nie udało się nawiązać połączenia z serwerem");
        setStatus("Nie udało się zarejestrować");
      }
      return Promise.reject(error);
    },
  );

  const onSubmit: SubmitHandler<FormData> = async (data) => {
    await axiosInstance
      .post("/api/users/register", data)
      .then((response) => {
        if (response.status === 200) {
          setStatus("Udało się zarejestrować");
          const formId = document.getElementById("form-id") as HTMLInputElement;
          const formLogin = document.getElementById(
            "form-login",
          ) as HTMLInputElement;
          const formPassword = document.getElementById(
            "form-password",
          ) as HTMLInputElement;
          const formPassword2 = document.getElementById(
            "form-password2",
          ) as HTMLInputElement;

          formId.value = "";
          formLogin.value = "";
          formPassword.value = "";
          formPassword2.value = "";
        } else {
          setStatus("Nie udało się zarejestrować");
        }
      })
      .catch((error: any) => {
        console.log(error);
        setStatus("Nie udało się zarejestrować");
      });
  };

  return (
    <div className="register__form">
      <div className="register__form-status">
        <h1>{status}</h1>
      </div>
      <form onSubmit={handleSubmit(onSubmit)} id="my-form">
        <div className="register__form-email">
          <label htmlFor="email"> email:</label>
          <br />
          <input
            id="form-id"
            {...register("email", {
              required: "This is required",
              minLength: 6,
            })}
          />
          <br />
          <p>{errors.email?.message}</p>
        </div>
        <div className="register__form-login">
          <label htmlFor="fname">Login:</label>
          <br />
          <input
            id="form-login"
            {...register("userName", {
              required: "This is required",
              minLength: 6,
            })}
          />
          <br />
          <p>{errors.userName?.message}</p>
        </div>
        <div className="register__form-password">
          <label htmlFor="lname">Hasło:</label>
          <br />
          <input
            id="form-password"
            type="password"
            {...register("password", {
              required: "This is required",
              minLength: 6,
            })}
          />
          <br />
          <p>{errors.password?.message}</p>
        </div>
        <div className="register__form-password-again">
          <label htmlFor="lname2"> Powtórz Hasło:</label>
          <br />
          <input
            id="form-password2"
            type="password"
            {...register("confirmPassword", {
              required: "This is required",
              minLength: 6,
            })}
          />
          <br />
          <p>{errors.confirmPassword?.message}</p>
        </div>
        <div className="register__form-button">
          <button type="submit">Zarejestruj</button>
        </div>
      </form>
    </div>
  );
};
