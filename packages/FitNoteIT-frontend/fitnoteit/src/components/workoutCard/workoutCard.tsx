import React, { useState } from "react";
import "./workoutCard.scss";
import List from "@mui/material/List";
import ListItem from "@mui/material/ListItem";
import ListItemText from "@mui/material/ListItemText";
import { Avatar, ListItemAvatar, ListItemIcon } from "@mui/material";
import DeleteIcon from "@mui/icons-material/Delete";

export const WorkoutCard: React.FC = () => {
  const [data, setData] = useState([
    [10, 10],
    [10, 10],
    [11, 11],
    [12, 12],
    [13, 13],
    [14, 14],
    [15, 15],
    [15, 15],
    [15, 15],
    [15, 15],
  ]);

  const [inputValue1, setInputValue1] = useState("");
  const [inputValue2, setInputValue2] = useState("");
  const handleAddValue = () => {
    console.log("inputValue1: " + inputValue1);
    console.log("inputValue2: " + inputValue2);
    console.log("inputValue2");
    if (inputValue1 !== "" && inputValue2 !== "") {
      const newValue = [parseInt(inputValue1), parseInt(inputValue2)];
      setData([...data, newValue]);
      setInputValue1("");
      setInputValue2("");
    }
  };

  const handleDeleteItem = (index: number) => {
    const newData = [...data];
    newData.splice(index, 1);
    setData(newData);
  };

  return (
    <div className="workout-page__workout-card">
      <div className="workout-page__workout-card-title">
        <h1>TRENING</h1>
        <h3>Przysiady</h3>
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
          maxHeight: "calc(100vh - 500px)",
          "& ul": { padding: 0 },
        }}
        subheader={<li />}
      >
        {data.map((item) => (
          <ListItem key={data.indexOf(item)}>
            <ListItemText primary={` ${item[0]}  x  ${item[1]} kg`} />
            <ListItemAvatar>
              <Avatar sx={{ background: "#dbddd2" }}>
                <DeleteIcon
                  sx={{ color: "#000", cursor: "pointer" }}
                  onClick={() => handleDeleteItem(data.indexOf(item))}
                />
              </Avatar>
            </ListItemAvatar>
          </ListItem>
        ))}
      </List>
      <div className="workout-page__workout-card-add">
        <div className="workout-page__workout-card-add-inputs">
          <input
            type="number"
            placeholder="powtórzenia"
            value={inputValue1}
            onChange={(e) => setInputValue1(e.target.value)}
          />
          <input
            type="number"
            placeholder="ciężar kg"
            value={inputValue2}
            onChange={(e) => setInputValue2(e.target.value)}
          />
        </div>

        <button type="button" onClick={handleAddValue}>
          Dodaj
        </button>
      </div>
      <div className="workout-page__workout-card-next-button">
        <button type="button">Nastepne ćwiczenie</button>
      </div>
    </div>
  );
};
