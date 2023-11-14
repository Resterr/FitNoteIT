import React, { useEffect, useState } from "react";
import "./history.scss";
import List from "@mui/material/List";
import ListItem from "@mui/material/ListItem";
import ListItemText from "@mui/material/ListItemText";
import { Avatar, ListItemAvatar, ListItemIcon } from "@mui/material";
import FitnessCenterIcon from "@mui/icons-material/FitnessCenter";
import axiosInstance from "../../utils/axiosInstance";
import { AxiosResponse } from "axios/index";
import { array } from "yup";
import { Link } from "react-router-dom";

type HistoryElement = {
  id: string;
  date: string;
  trainingDetails: object[];
};

export const History: React.FC = () => {
  const [data, setData] = useState<HistoryElement[]>([]);
  const currentUser = localStorage.getItem("currentUser");
  useEffect(() => {
    let token: string | null = localStorage.getItem("accessToken");
    let config2 = {
      headers: { Authorization: `Bearer ${token}` },
    };
    axiosInstance
      .get<HistoryElement[]>("/api/trainings/history", config2)
      .then((response: AxiosResponse<HistoryElement[]>) => {
        if (response.data.length !== 0) {
          setData(response.data);
        }
      })
      .catch((error) => {
        console.log(error);
      });
  }, []);
  return (
    <div className="history-page__history">
      <div className="history-page__history-title">
        <h1>TWOJA HISTORIA</h1>
        {data.length == 0 && <h3>BRAK TRENINGÓW</h3>}
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
          <Link to={`/workouthistory/${item.id}`} key={data.indexOf(item)}>
            <ListItem>
              <ListItemText primary={item.date} />
              <ListItemAvatar>
                <Avatar sx={{ background: "#dbddd2" }}>
                  <FitnessCenterIcon sx={{ color: "#000" }} />
                </Avatar>
              </ListItemAvatar>
            </ListItem>
          </Link>
        ))}
      </List>
    </div>
  );
};
