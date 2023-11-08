import * as React from "react";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import Modal from "@mui/material/Modal";
import TextField from "@mui/material/TextField";
import axiosInstance from "../../utils/axiosInstance";
import { useNavigate } from "react-router-dom";
import { useEffect } from "react";
import { createTheme, ThemeProvider } from "@mui/material";

const style = {
  position: "absolute" as "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: 400,
  bgcolor: "#151512",
  border: "2px solid #000",
  boxShadow: 24,
  color: "#b17a41",
  p: 4,
  display: "flex",
  flexDirection: "column",
};
type Exercise = {
  name: string;
  categoryName: string;
  id: number;
};

type ModalYourPlan = {
  exercises: Exercise[];
};

const inputStyle = {
  color: "#b17a41",
};

const labelStyle = {
  color: "#b17a41",
};

export const ModalYourPlan: React.FC<ModalYourPlan> = ({ exercises }) => {
  const [open, setOpen] = React.useState(false);
  const [exerciseName, setExerciseName] = React.useState("");
  const [data, setData] = React.useState<Exercise[]>(exercises);
  const navigate = useNavigate();
  const handleOpen = () => {
    setOpen(true);
    setExerciseName("");
  };

  useEffect(() => {
    setData(exercises);
  }, [exercises]);
  const handleClose = () => setOpen(false);

  const handleSave = () => {
    let token: string | null = localStorage.getItem("accessToken");
    let config2 = {
      headers: { Authorization: `Bearer ${token}` },
    };

    if (data.length > 0 && exerciseName !== "") {
      axiosInstance
        .post(
          "/api/workouts/plans",
          {
            name: exerciseName,
            exercises: data.map((item) => item.id),
          },
          config2,
        )
        .then((response) => {
          handleClose();
          alert("Zapisano");
          navigate("/plans");
        })
        .catch((error) => {
          console.error(error);
          alert("Blad");
        });
    } else {
      alert("Brak ćwiczen lub nie podałeś nazwy");
    }
  };

  return (
    <>
      <button
        className="add-plan-page-content__your-plan-submit"
        onClick={handleOpen}
      >
        ZAPISZ
      </button>
      <Modal
        open={open}
        onClose={handleClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <div className="myModal">
          <Box sx={style}>
            <Typography id="modal-modal-title" variant="h6" component="h2">
              Dodaj plan
            </Typography>
            <TextField
              style={inputStyle}
              label="Nazwa planu"
              variant="outlined"
              value={exerciseName}
              InputLabelProps={{ style: labelStyle }}
              onChange={(e) => setExerciseName(e.target.value)}
              sx={{ mt: 2 }}
            />
            <Button variant="contained" onClick={handleSave} sx={{ mt: 2 }}>
              Zapisz
            </Button>
          </Box>
        </div>
      </Modal>
    </>
  );
};
