import React, { useEffect, useState } from "react";
import "./plans.scss";
import List from "@mui/material/List";
import ListItem from "@mui/material/ListItem";
import ListItemText from "@mui/material/ListItemText";
import { Avatar, ListItemAvatar } from "@mui/material";
import FitnessCenterIcon from "@mui/icons-material/FitnessCenter";
import { Link } from "react-router-dom";
import axiosInstance from "../../utils/axiosInstance";
import { AxiosResponse } from "axios";

type Plan = {
  id: string;
  name: string;
  exercises: any[];
};
export const Plans: React.FC = () => {
  const [data, setData] = useState<Plan[]>([]);
  useEffect(() => {
    let token: string | null = localStorage.getItem("accessToken");
    let config2 = {
      headers: { Authorization: `Bearer ${token}` },
    };

    axiosInstance
      .get<Plan[]>("/api/workouts/plans", config2)
      .then((response: AxiosResponse<Plan[]>) => {
        if (response.data.length !== 0) {
          setData(response.data);
        }
      })
      .catch((error) => {
        console.log(error);
      });
  }, []);
  return (
    <div className="plans-page__plans">
      <div className="plans-page__plans-title">
        <h1>TWOJE PLANY TRENINGOWE</h1>
      </div>
      <List
        sx={{
          marginY: "auto",
          width: "100%",
          maxWidth: "calc(100% - 40px)",
          bgcolor: "#1b1b1b44",
          color: "#dbddd2",
          position: "relative",
          overflow: "auto",
          margin: "10px",
          marginX: "20px",
          maxHeight: "calc(100vh - 340px)",
          "& ul": { padding: 0 },
        }}
        subheader={<li />}
      >
        {data.map((item) => (
          <Link to={`/workout/${item.id}`} key={data.indexOf(item)}>
            <ListItem>
              <ListItemText primary={item.name} />
              <ListItemAvatar>
                <Avatar sx={{ background: "#dbddd2" }}>
                  <FitnessCenterIcon sx={{ color: "#000" }} />
                </Avatar>
              </ListItemAvatar>
            </ListItem>
          </Link>
        ))}
      </List>
      <div className="plans-page__plans-add-plan-button">
        <Link to="/addplan">
          <button type="button">Dodaj Plan</button>
        </Link>
      </div>
    </div>
  );
};
