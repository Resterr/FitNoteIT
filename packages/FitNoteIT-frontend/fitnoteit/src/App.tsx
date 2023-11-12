import React, { useContext, useEffect, useState } from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import "./App.css";
import axios, { AxiosResponse } from "axios";
import { UsersContext, UsersContextType } from "./contexts/user.context";
import { Home } from "./pages/home";
import { Navbar } from "./components/navbar";
import { Login } from "./pages/login";
import { Register } from "./pages/register";
import axiosInstance from "./utils/axiosInstance";
import { AdminPanel } from "./pages/adminPanel/adminPanel";
import { Records } from "./pages/records";
import { PlansPage } from "./pages/plansPage";
import { HistoryPage } from "./pages/historyPage";
import { AddPlanPage } from "./pages/addPlanPage";
import { WorkoutHistory } from "./pages/workoutHistory";
import { WorkoutPage } from "./pages/workoutPage";

function App() {
  const { setCurrentUserFromContext } = useContext(
    UsersContext,
  ) as UsersContextType;
  const currentUser = localStorage.getItem("currentUser");
  const [intervalToken, setIntervalToken] = useState<
    number | null | NodeJS.Timer
  >(null);
  const logout = () => {
    localStorage.setItem("currentUser", "");
    localStorage.setItem("accessToken", "");
    localStorage.setItem("refreshToken", "");
    setCurrentUserFromContext("");
  };

  axios.interceptors.response.use(
    (response) => response,
    (error) => {
      if (error.response) {
        switch (error.response.status) {
          case 400:
            logout();
            console.log("Nieprawidłowe żądanie");
            break;
          case 401:
            logout();
            console.log("Brak autoryzacji");
            break;
          case 404:
            logout();
            console.log("Nie znaleziono zasobu");
            break;
          default:
            logout();
            console.log(`Wystąpił błąd: ${error.response.status}`);
        }
      } else {
        logout();
        console.log("Nie udało się nawiązać połączenia z serwerem");
      }
      return Promise.reject(error);
    },
  );

  useEffect(() => {
    const tokenDateStr = localStorage.getItem("tokenDate");
    const tokenDate = parseInt(tokenDateStr as string, 10);
    let today = Date.now();
    const TWENTY_THREE_HOURS = 23 * 60 * 60 * 1000; // 23 hours in milliseconds

    if (today - tokenDate > TWENTY_THREE_HOURS) {
      logout();
    }
    const intervalId = setInterval(refreshTokens, 60000);
    setIntervalToken(intervalId);
    return () => clearInterval(intervalId);
  }, []);

  useEffect(() => {
    if (currentUser) {
      setCurrentUserFromContext(currentUser);
    } else {
      setCurrentUserFromContext(undefined);
    }
  }, []);

  const refreshTokens = async () => {
    let rToken = localStorage.getItem("refreshToken");
    let aToken = localStorage.getItem("accessToken");
    let data = {
      accessToken: aToken,
      refreshToken: rToken,
    };

    let config2: {
      headers: { Authorization: string };
    } = {
      headers: { Authorization: `Bearer ${aToken}` },
    };

    if (
      currentUser !== undefined &&
      currentUser !== null &&
      currentUser !== ""
    ) {
      console.log(data);
      await axiosInstance
        .post("/api/users/token/refresh", data, config2)
        .then((response: AxiosResponse<any, any>) => {
          if (response.status == 200) {
            localStorage.setItem("accessToken", response.data.accessToken);
            localStorage.setItem("refreshToken", response.data.refreshToken);
            let myDate = Date.now();
            localStorage.setItem("tokenDate", myDate.toString());
            console.log(response);
          } else {
            console.log("złe dane do odswieźenia tokenów");
          }
        })
        .catch((error: any) => {
          console.log(error);
        });
    }
  };
  return (
    <div className="App">
      <BrowserRouter>
        <Routes>
          <Route element={<Navbar />}>
            <Route index element={<Home />} />
            <Route path="*" element={<Home />} />
            <Route path="login" element={<Login />} />
            <Route path="register" element={<Register />} />
            <Route path="admin" element={<AdminPanel />} />
            <Route path="records" element={<Records />} />
            <Route path="addplan" element={<AddPlanPage />} />
            <Route path="history" element={<HistoryPage />} />
            <Route path="workouthistory/:id" element={<WorkoutHistory />} />
            <Route path="workout/:id" element={<WorkoutPage />} />
            <Route path="plans" element={<PlansPage />} />
          </Route>
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
