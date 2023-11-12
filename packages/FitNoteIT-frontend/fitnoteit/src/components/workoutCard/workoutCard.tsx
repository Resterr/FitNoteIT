import React, { useEffect, useState } from "react";
import "./workoutCard.scss";
import List from "@mui/material/List";
import ListItem from "@mui/material/ListItem";
import ListItemText from "@mui/material/ListItemText";
import { Avatar, ListItemAvatar, ListItemIcon } from "@mui/material";
import DeleteIcon from "@mui/icons-material/Delete";
import axiosInstance from "../../utils/axiosInstance";
import { AxiosResponse } from "axios/index";
import { useNavigate } from "react-router-dom";
type Plan = {
  id: string;
  name: string;
  exercises: any[];
};

type Exercise = {
  exercise: { id: string; name: string; exercises: any[] };
  series: ArrayOfNumbersArrays;
};

type Traning = {
  date: Date;
  details: Exercise[];
};

type WorkoutCardProps = {
  id: string;
};
type ArrayOfNumbersArrays = number[][];

export const WorkoutCard: React.FC<WorkoutCardProps> = ({ id }) => {
  const [plan, setPlan] = useState<Plan>();
  const [traning, setTraning] = useState<Traning>({
    date: new Date(),
    details: [],
  });
  const [index, setIndex] = useState<number>(0);
  const [exercisesName, setExercisesName] = useState<string>("");
  const [data, setData] = useState<ArrayOfNumbersArrays>([]);
  const [inputValue1, setInputValue1] = useState("");
  const [inputValue2, setInputValue2] = useState("");
  const navigate = useNavigate();

  useEffect(() => {
    let token: string | null = localStorage.getItem("accessToken");
    let config2 = {
      headers: { Authorization: `Bearer ${token}` },
    };

    axiosInstance
      .get<Plan>(`/api/workouts/plans/${id}`, config2)
      .then((response: AxiosResponse<Plan>) => {
        setPlan(response.data);
        setExercisesName(response.data.exercises[0].name);
      })
      .catch((error) => {
        console.log(error);
      });
  }, []);

  const handleAddValue = () => {
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

  const handleNextExercise = () => {
    let tmpPartOfTraning = traning;
    let tmpExercise = { exercise: plan?.exercises[index], series: data };
    tmpPartOfTraning.details.push(tmpExercise);
    setTraning(tmpPartOfTraning);
    if (plan && plan.exercises && index + 1 < plan.exercises.length) {
      setExercisesName(plan.exercises[index + 1].name);
      setIndex(index + 1);
      setData([]);
    } else {
      let token: string | null = localStorage.getItem("accessToken");
      let config2 = {
        headers: { Authorization: `Bearer ${token}` },
      };

      let data = traning.details.map((item) => ({
        exerciseId: item.exercise.id,
        series: item.series,
      }));

      let sendData = {
        date: traning.date.toLocaleDateString("pl-PL", {
          day: "2-digit",
          month: "2-digit",
          year: "numeric",
        }),
        details: data,
      };

      console.log("send");
      console.log(sendData);
      console.log("send");

      axiosInstance
        .post("/api/trainings/", sendData, config2)
        .then((response: AxiosResponse<Plan[]>) => {
          console.log(response);
        })
        .catch((error) => {
          console.log(error);
        });

      alert("Skończyłeś trening");
      navigate("/");
    }
  };

  return (
    <div className="workout-page__workout-card">
      {plan && (
        <>
          <div className="workout-page__workout-card-title">
            <h1>TRENING</h1>
            <h3>{exercisesName && exercisesName}</h3>
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
            <button type="button" onClick={handleNextExercise}>
              Nastepne ćwiczenie
            </button>
          </div>
        </>
      )}
    </div>
  );
};
