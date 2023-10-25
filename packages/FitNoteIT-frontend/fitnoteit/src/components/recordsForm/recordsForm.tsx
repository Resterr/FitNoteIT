import React, { useState, useEffect, ReactNode, Ref } from "react";
import "./recordsForm.scss";
import { useForm, SubmitHandler, UseFormRegister } from "react-hook-form";
import axios, { AxiosResponse } from "axios";
import { useNavigate } from "react-router-dom";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { ModalRecordsX } from "../modalRecordsX/modalRecordsX";
import { CustomDatePicker } from "../modalRecordsX";

interface Exercise {
  exerciseName: string;
  recordDate: Date;
  result: number;
}

export const RecordsForm: React.FC = () => {
  const currentUser = localStorage.getItem("currentUser");
  let navigate = useNavigate();
  const {
    register,
    handleSubmit,
    watch,
    formState: { errors },
  } = useForm();
  const [startDate, setStartDate] = useState<Date>(new Date());
  const [startWeight, setStartWeight] = useState<number>(0);
  const [selectedValue, setSelectedValue] = useState<string>("Martwy Ciąg");

  useEffect(() => {
    if (!currentUser) {
      navigate("/");
    }
  }, []);

  const [exercises, setExercises] = useState<Exercise[]>([
    {
      exerciseName: "Martwy Ciąg",
      recordDate: new Date(),
      result: 0,
    },
  ]);

  useEffect(() => {
    let token = localStorage.getItem("accessToken");
    let config2 = {
      headers: { Authorization: `Bearer ${token}` },
    };
    axios
      .get<Exercise[]>(
        "https://fitnoteit.azurewebsites.net/api/records?PageSize=10&PageNumber=1",
        config2,
      )
      .then((response: AxiosResponse<Exercise[]>) => {
        if (response.status === 200) {
          response.data.forEach((element) => {
            if (
              element.recordDate !== null &&
              element.recordDate.toString() !== ""
            ) {
              element.recordDate = new Date(element.recordDate);
            }
            if (element.recordDate.toString() === "Martwy ciąg") {
              element.exerciseName = "Martwy Ciąg";
            }
          });
          console.log(response);
          setExercises(response.data);
          setStartDate(response.data[0].recordDate);
          let input = document.getElementById("weight") as HTMLInputElement;
          input.value = response.data[0].result.toString();
          setStartWeight(response.data[0].result);
        } else {
          alert("nie pobrano twoich danych");
        }
      });
  }, []);

  useEffect(() => {
    const selectedExerciseData = exercises.find(
      (exercise) => exercise.exerciseName === selectedValue,
    );
    setStartDate(selectedExerciseData?.recordDate || new Date());
    let inp = document.getElementById("weight") as HTMLInputElement;
    inp.value = selectedExerciseData?.result.toString() || "0";
    setStartWeight(selectedExerciseData?.result || 0);
  }, [selectedValue]);

  const onSubmit: SubmitHandler<Record<string, any>> = async (data) => {
    let token = localStorage.getItem("accessToken");
    let config = {
      headers: { Authorization: `Bearer ${token}` },
    };
    console.log(data);
    data["recordDate"] = startDate;
    data["exerciseName"] = selectedValue;
    data["result"] = startWeight;
    await axios
      .put("https://fitnoteit.azurewebsites.net/api/records", data, config)
      .then((response) => {
        if (response.status === 200) {
          alert("Zapisano");
        } else {
          alert("Błąd");
        }
      });
  };

  return (
    <div className="records__form">
      <div className="records__form-all">
        <div className="records__form-title">
          <h1>Twoje rekordy</h1>
        </div>
        <form onSubmit={handleSubmit(onSubmit)} id="my-form">
          <div className="records__form-exercise">
            <label htmlFor="exercise">Ćwiczenie:</label>
            <br />
            <select
              value={selectedValue}
              onChange={(v) => setSelectedValue(v.target.value)}
            >
              {exercises.map((exercise) => (
                <option
                  key={exercise.exerciseName}
                  value={exercise.exerciseName}
                >
                  {exercise.exerciseName}
                </option>
              ))}
            </select>
            <p>
              {typeof errors.exercise?.message === "string"
                ? errors.exercise.message
                : null}
            </p>
          </div>
          <div className="records__form-date">
            <label htmlFor="date">Data:</label>
            <br />
            <CustomDatePicker
              selected={startDate}
              onChange={(date: Date) => {
                console.log(date);
                setStartDate(date);
              }}
              dateFormat="dd/MM/yyyy"
              exerciseName="date"
            />
            <br />

            <p>
              {typeof errors.date?.message === "string"
                ? errors.date.message
                : null}
            </p>
          </div>
          <div className="records__form-weight">
            <label htmlFor="weight">Ciężar/Powtórzenia:</label>
            <br />
            <input
              id="weight"
              type="text"
              onChange={(v) => setStartWeight(parseInt(v.target.value) || 0)}
            />
            <br />

            <p>
              {typeof errors.weight?.message === "string"
                ? errors.weight.message
                : null}
            </p>
          </div>

          <div className="records__form-button">
            <button type="submit">Zapisz</button>
            <ModalRecordsX data={selectedValue}></ModalRecordsX>
          </div>
        </form>
      </div>
    </div>
  );
};
