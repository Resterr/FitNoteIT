import React, {useContext, useEffect, useState} from "react";
import {BrowserRouter, Routes, Route} from "react-router-dom";
import logo from "./logo.svg";
import "./App.css";

import axios from "axios";
import {UsersContext, UsersContextType} from "./contexts/user.context";
import {Home} from "./pages/home";
import {Navbar} from "./components/navbar";
import {Login} from "./pages/login";
import {Register} from "./pages/register";
import axiosInstance from "./utils/axiosInstance";
import {AdminPanel} from "./pages/adminPanel/adminPanel";
import {sassNull} from "sass";

function App() {
    const {setCurrentUser2} = useContext(UsersContext) as UsersContextType;
    const currentUser = localStorage.getItem("currentUser");
    const [intervalToken, setIntervalToken] = useState<
        number | null | NodeJS.Timer
    >(null);
    const logout = () => {
        localStorage.setItem("currentUser", "");
        localStorage.setItem("accessToken", "");
        localStorage.setItem("refreshToken", "");
        setCurrentUser2("");
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
        }
    );

    useEffect(() => {
        const tokenDateStr = localStorage.getItem("tokenDate");
        const tokenDate = parseInt(tokenDateStr as string, 10);
        let today = Date.now();
        const TWENTY_THREE_HOURS = 23 * 60 * 60 * 1000; // 23 hours in milliseconds

        if (today - tokenDate > TWENTY_THREE_HOURS) {
            logout();
        }
        const intervalId = setInterval(refreshTokens, 300000);
        setIntervalToken(intervalId);
        return () => clearInterval(intervalId);
    }, []);

    useEffect(() => {
        if (currentUser) {
            setCurrentUser2(currentUser);
        } else {
            setCurrentUser2(undefined);
        }
    }, []);

    const refreshTokens = async () => {
        let rToken = localStorage.getItem("refreshToken");
        let aToken = localStorage.getItem("accessToken");
        let data = {
            accessToken: aToken,
            refreshToken: rToken,
        };

        if (
            currentUser !== undefined &&
            currentUser !== null &&
            currentUser !== ""
        ) {
            await axiosInstance.post("/api/token/refresh", data).then((response) => {
                if (response.status == 200) {
                    localStorage.setItem("accessToken", response.data.accessToken);
                    localStorage.setItem("refreshToken", response.data.refreshToken);
                    let myDate = Date.now();
                    localStorage.setItem("tokenDate", myDate.toString());
                    console.log(response);
                } else {
                    console.log("złe dane do odswieźenia tokenów");
                }
            });
        }
    };
    return (
        <div className="App">
            <BrowserRouter>
                <Routes>
                    <Route element={<Navbar/>}>
                        <Route index element={<Home/>}/>
                        <Route path="login" element={<Login/>}/>
                        <Route path="register" element={<Register/>}/>
                        <Route path="admin" element={<AdminPanel/>}/>
                        {/* <Route path="records" element={<Records />} /> */}
                    </Route>
                </Routes>
            </BrowserRouter>
        </div>
    );
}

export default App;
