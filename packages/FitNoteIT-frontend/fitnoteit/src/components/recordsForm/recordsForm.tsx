import React, { useState, useEffect, ReactNode, Ref } from "react";
import "./recordsForm.scss";
import { useForm, SubmitHandler, UseFormRegister } from "react-hook-form";
import axios, { AxiosResponse } from "axios";
import { useNavigate } from "react-router-dom";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { ModalRecordsX } from "../modalRecordsX/modalRecordsX";
import { CustomDatePicker } from "../modalRecordsX";
import axiosInstance from "../../utils/axiosInstance";

type Exercise = {
  name: string;
  categoryName: string;
  id: number;
};

type Record = {
  exerciseId: string;
  exerciseName: string;
  result: number;
  recordDate: Date;
};

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
  const [startWeight, setStartWeight] = useState<string>("0");

  useEffect(() => {
    if (!currentUser) {
      navigate("/");
    }
  }, [currentUser, navigate]);

  const [exercises, setExercises] = useState<Exercise[]>([]);
  const [exercisesRecords, setExercisesRecords] = useState<Record[]>([]);
  const [selectedValue, setSelectedValue] = useState<string>("");
  const [selectedValueId, setSelectedValueId] = useState<string>("");
  const [recordsGet, setRecordsGet] = useState<boolean>(false);

  useEffect(() => {
    if (selectedValue !== "" && exercises.length !== 0) {
      let tmpId = exercises.find((element) => element.name === selectedValue);
      if (tmpId !== undefined) {
        setSelectedValueId(tmpId.id.toString());
      }
    }
  }, [selectedValue]);

  useEffect(() => {
    let token = localStorage.getItem("accessToken");
    let config2 = {
      headers: { Authorization: `Bearer ${token}` },
    };
    try {
      axiosInstance
        .get<Record[]>("/api/records", config2)
        .then((response: AxiosResponse<Record[]>) => {
          if (response.data.length !== 0) {
            // setExercisesRecords(response.data);
          }
          setRecordsGet(true);
        });
    } catch (e) {
      console.error(e);
    }
  }, []);

  useEffect(() => {
    if (exercisesRecords.length > 0) {
      let input = document.getElementById("weight") as HTMLInputElement;
      input.value = exercisesRecords[0].result.toString();
      setStartWeight(exercisesRecords[0].result.toString());
      let tmpExercise = exercises.find(
        (x) => x.name === exercisesRecords[0].exerciseName,
      );
      if (tmpExercise) {
        setSelectedValueId(tmpExercise.id.toString());
      }
    }
  }, [exercisesRecords]);

  useEffect(() => {
    if (recordsGet === true) {
      let token = localStorage.getItem("accessToken");
      let config2 = {
        headers: { Authorization: `Bearer ${token}` },
      };
      try {
        axiosInstance
          .get<Exercise[]>("/api/exercises", config2)
          .then((response: AxiosResponse<Exercise[]>) => {
            if (response.status === 200) {
              const newExercises = response.data;
              setExercises(newExercises);

              const uniqueExercises = new Set(
                newExercises.map((exercise) => exercise.name),
              );

              const updatedRecords = [...exercisesRecords];

              uniqueExercises.forEach((exerciseName) => {
                if (
                  !updatedRecords.some(
                    (record) => record.exerciseName === exerciseName,
                  )
                ) {
                  updatedRecords.push({
                    exerciseId: "",
                    exerciseName: exerciseName,
                    result: 0,
                    recordDate: new Date(),
                  });

                  setExercisesRecords(updatedRecords);
                }
              });

              if (exercisesRecords[0]) {
                setStartDate(exercisesRecords[0].recordDate);
                setStartWeight(exercisesRecords[0].result.toString());
                setSelectedValue(exercisesRecords[0].exerciseName);
              } else {
                setStartDate(new Date());
                setStartWeight("0");
              }
            } else {
              alert("Nie pobrano twoich danych");
            }
          });
      } catch (error) {
        console.error(error);
      }
    }
  }, [recordsGet]);

  useEffect(() => {
    if (exercisesRecords.length !== 0) {
      const selectedExerciseData = exercisesRecords.find(
        (exercisesRecord) => exercisesRecord.exerciseName === selectedValue,
      );
      setStartDate(selectedExerciseData?.recordDate || new Date());
      let inp = document.getElementById("weight") as HTMLInputElement;
      inp.value = selectedExerciseData?.result.toString() || "0";
      setStartWeight(selectedExerciseData?.result.toString() || "0");
    }
  }, [selectedValue]);

  const onSubmit: SubmitHandler<any> = async (data) => {
    let token = localStorage.getItem("accessToken");
    let config = {
      headers: { Authorization: `Bearer ${token}` },
    };

    let dataToSave = {
      exerciseId: selectedValueId,
      result: startWeight,
      recordDate: startDate,
    };
    console.log(dataToSave);

    try {
      // await axiosInstance.put("/api/records", dataToSave, config);
      alert("Zapisano");
    } catch (error) {
      console.error("Błąd zapytania Axios:", error);
      alert("Błąd");
    }
  };

  return (
    <div className="records__form">
      <div className="records__form-all">
        <div className="records__form-title">
          <h1>Twoje rekordy</h1>
        </div>
        {exercisesRecords.length == 0 ? (
          <div className="records__form-loading">Ładowanie...</div>
        ) : (
          <form onSubmit={handleSubmit(onSubmit)} id="my-form">
            <div className="records__form-exercise">
              <label htmlFor="exercise">Ćwiczenie:</label>
              <br />
              <select
                value={selectedValue}
                onChange={(v) => setSelectedValue(v.target.value)}
              >
                {exercisesRecords.map((exercise) => (
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
                onChange={(v) => setStartWeight(v.target.value || "0")}
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
              <ModalRecordsX data={selectedValueId}></ModalRecordsX>
            </div>
          </form>
        )}
      </div>
    </div>
  );
};
