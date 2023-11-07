import * as React from "react";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import Modal from "@mui/material/Modal";
import "../recordsForm/recordsForm.scss";
import "./modalRecorsX.scss";
import axios, { AxiosResponse } from "axios";
import axiosInstance from "../../utils/axiosInstance";
import { useNavigate } from "react-router-dom";

const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: 400,
  bgcolor: "#151512",
  border: "2px solid #b17a41",
  color: "#b17a41",
  boxShadow: 24,
  display: "flex",
  flexDirection: "column",
  justifyContent: "center",
  alignItems: "center",
  p: 4,
};

interface ModalRecordsXProps {
  data: string;
}

export const ModalRecordsX: React.FC<ModalRecordsXProps> = (props) => {
  const [open, setOpen] = React.useState<boolean>(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);
  const navigate = useNavigate();

  const removeHandler = (data: string) => {
    let token: string | null = localStorage.getItem("accessToken");
    let config = {
      headers: { Authorization: `Bearer ${token}` },
    };

    if (data == "") {
      alert("Rekord nie istnieje");
    } else {
      axiosInstance
        .delete(`/api/records/${data}`, config)
        .then((response: AxiosResponse<any, any>): void => {
          if (response.status == 204) {
            alert("Usunięto");
            window.location.reload();
          } else {
            alert("Błąd");
            handleClose();
          }
        })
        .catch((error) => {
          console.log(error);
          alert("Nie udalo sie usunac");
          handleClose();
        });
    }
  };

  return (
    <div>
      <Button onClick={handleOpen} className="records__form-button-x">
        X
      </Button>
      <Modal
        open={open}
        onClose={handleClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box sx={style}>
          <Typography id="modal-modal-title" variant="h6" component="h2">
            Czy chcesz usunąć rekord?
          </Typography>
          <Typography id="modal-modal-description" sx={{ mt: 2 }}>
            <button
              type="button"
              onClick={() => removeHandler(props.data)}
              className="records__form-button-inside"
            >
              Usuń
            </button>
          </Typography>
        </Box>
      </Modal>
    </div>
  );
};
