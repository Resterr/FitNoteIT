import React, { useEffect, useState } from "react";
import "./addPlanPage.scss";
import { Footer } from "../../components/footer";
import { AvailableExercises } from "../../components/availableExercises";
import plansGym from "../../utils/plansgym.png";
import { YourPlan } from "../../components/yourPlan";
import { useNavigate } from "react-router-dom";
import axiosInstance from "../../utils/axiosInstance";
import { AxiosResponse } from "axios";

type Exercise = {
  name: string;
  categoryName: string;
  id: number;
};

export const AddPlanPage: React.FC = () => {
  const currentUser = localStorage.getItem("currentUser");
  const navigate = useNavigate();
  const [exercises, setExercises] = useState<Exercise[]>([]);
  const [yourExercises, setYourExercises] = useState<Exercise[]>([]);
  useEffect(() => {
    if (!currentUser) {
      navigate("/");
    }
  }, [currentUser, navigate]);

  useEffect(() => {
    let token: string | null = localStorage.getItem("accessToken");
    let config2 = {
      headers: { Authorization: `Bearer ${token}` },
    };

    axiosInstance
      .get<Exercise[]>("/api/exercises", config2)
      .then((response: AxiosResponse<Exercise[]>) => {
        if (response.data.length !== 0) {
          setExercises(response.data);
        }
      })
      .catch((error) => {
        console.log(error);
      });
  }, []);

  const addHandle = (exercise: Exercise) => {
    if (!yourExercises.some((item) => item.id === exercise.id)) {
      let exercisesTmp: Exercise[] = [...yourExercises, exercise];
      setYourExercises(exercisesTmp);
    }
  };
  const deleteHandle = (exercise: Exercise) => {
    let exercisesTmp: Exercise[] = yourExercises.filter(
      (item) => item.id !== exercise.id,
    );
    setYourExercises(exercisesTmp);
  };
  return (
    <div className="add-plan-page">
      <div className="add-plan-page-content">
        <AvailableExercises
          exercises={exercises}
          categories={[
            "Klatka piersiowa",
            "Triceps",
            "Plecy",
            "Biceps",
            "Nogi",
            "Brzuch",
          ]}
          addHandle={addHandle}
        />
        <img className="add-plan-page-photo" src={plansGym} alt="gym" />
        <YourPlan exercises={yourExercises} deleteHandle={deleteHandle} />
      </div>
      <Footer />
    </div>
  );
};
