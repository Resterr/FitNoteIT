import React, { useEffect, useState } from "react";
import "./adminPanel.scss";
import CustomPaginationActionsTable from "../../components/customPaginationActionsTable/customPaginationActionsTable";
import axiosInstance from "../../utils/axiosInstance";
import axios, { AxiosResponse } from "axios";
import { useNavigate } from "react-router-dom";

type RoleType = [{ name: string }];
export const AdminPanel: React.FC = () => {
  const [roles, setRoles] = useState<RoleType | undefined>(undefined);
  const currentUser = localStorage.getItem("currentUser");
  const navigate = useNavigate();

  useEffect(() => {
    if (!currentUser) {
      navigate("/");
    }
  }, [currentUser, navigate]);

  useEffect(() => {
    if (
      roles &&
      !roles.some((role) => role.name === "Admin" || role.name === "SuperAdmin")
    ) {
      navigate("/");
    }
  }, [roles]);
  axios.interceptors.response.use(
    (response: AxiosResponse<any, any>) => response,
    (error) => {
      if (error.response) {
        switch (error.response.status) {
          case 400:
            console.log("Nieprawidłowe żądanie");

            break;
          case 404:
            console.log("Nie znaleziono zasobu");

            break;
            break;
          case 403:
            console.log("403");

            break;
          default:
            console.log(`Wystąpił błąd: ${error.response.status}`);
        }
      } else {
        console.log("Nie udało się nawiązać połączenia z serwerem");
      }
      return Promise.reject(error);
    },
  );
  const UserCheck = async () => {
    let token: string | null = localStorage.getItem("accessToken");
    let config2: { headers: { Authorization: string } } = {
      headers: { Authorization: `Bearer ${token}` },
    };

    try {
      await axiosInstance
        .get("/api/users/current", config2)
        .then((response: AxiosResponse<any, any>): void => {
          if (response.status == 200) {
            console.log(response);
            setRoles(response.data.roles);
          } else {
            console.log("blad sprawdzania usera");
          }
        });
    } catch (error) {
      console.log(error);
    }
  };
  useEffect(() => {
    UserCheck();
  }, []);
  return (
    <div className="adminPanel">
      {roles &&
      (roles.some((role) => role.name === "Admin") ||
        roles.some((role) => role.name === "SuperAdmin")) ? (
        <CustomPaginationActionsTable />
      ) : null}
    </div>
  );
};
