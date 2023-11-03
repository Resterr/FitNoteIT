import React, { useState } from "react";
import "./plans.scss";
import List from "@mui/material/List";
import ListItem from "@mui/material/ListItem";
import ListItemText from "@mui/material/ListItemText";
import { Avatar, ListItemAvatar, ListItemIcon } from "@mui/material";
import FitnessCenterIcon from "@mui/icons-material/FitnessCenter";

export const Plans: React.FC = () => {
  const [data, setData] = useState([1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3]);

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
          <ListItem key={`item-${item}-${item}`}>
            <ListItemText primary={`Item ${item}`} />
            <ListItemAvatar>
              <Avatar sx={{ background: "#dbddd2" }}>
                <FitnessCenterIcon sx={{ color: "#000" }} />
              </Avatar>
            </ListItemAvatar>
          </ListItem>
        ))}
      </List>
      <div className="plans-page__plans-add-plan-button">
        <button type="button">Dodaj Plan</button>
      </div>
    </div>
  );
};
