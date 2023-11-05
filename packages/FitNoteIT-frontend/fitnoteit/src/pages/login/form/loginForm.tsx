import React, { useState, useContext, useEffect } from "react";
import "./loginForm.scss";
import { useForm, SubmitHandler } from "react-hook-form";
import axios, { AxiosResponse } from "axios";
import { UsersContext, UsersContextType } from "../../../contexts/user.context";
import { useNavigate } from "react-router-dom";
import axiosInstance from "../../../utils/axiosInstance";

type FormData = {
  userName: string;
  password: string;
};

export const LoginForm: React.FC = () => {
  const currentUser: string | null | undefined =
    localStorage.getItem("currentUser");
  const { currentUserFromContext, setCurrentUserFromContext } = useContext(
    UsersContext,
  ) as UsersContextType;
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<FormData>();
  const [status, setStatus] = useState<string | null>(null);
  let navigate = useNavigate();

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
            setStatus("Nie udało się zalogować");
            break;
          case 401:
            console.log("Brak autoryzacji");
            setStatus("Nie udało się zalogować");
            break;
          case 404:
            console.log("Nie znaleziono zasobu");
            setStatus("Nie udało się zalogować");
            break;
          default:
            console.log(`Wystąpił błąd: ${error.response.status}`);
        }
      } else {
        console.log("Nie udało się nawiązać połączenia z serwerem");
        setStatus("Nie udało się zalogować");
      }
      return Promise.reject(error);
    },
  );

  const onSubmit: SubmitHandler<FormData> = async (data: FormData) => {
    await axiosInstance
      .post("/api/users/login", data)
      .then((response) => {
        if (response.status === 200) {
          localStorage.setItem("currentUser", data.userName);
          localStorage.setItem("accessToken", response.data.accessToken);
          localStorage.setItem("refreshToken", response.data.refreshToken);
          let myDate = Date.now();
          localStorage.setItem("tokenDate", myDate.toString());
          setCurrentUserFromContext(data.userName);
          setStatus(`Witaj ${data.userName}!`);
          console.log(response);
          navigate("/");
        } else {
          setStatus("Nie udało się zalogować");
        }
      })
      .catch((e) => {
        console.error(e);
        setStatus("Wystąpił błąd podczas logowania");
      });
  };

  return (
    <div className="login__form">
      <div className="login__form-status">
        <h1>{status}</h1>
      </div>
      <form onSubmit={handleSubmit(onSubmit)}>
        <div className="login__form-login">
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
        </div>
        <div className="login__form-password">
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
        </div>
        <div className="login__form-button">
          <button type="submit">Zaloguj</button>
        </div>
      </form>
    </div>
  );
};
