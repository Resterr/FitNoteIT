import React, { useEffect, useState } from "react";
import "./workoutHistoryPanel.scss";
import { WorkoutHistoryTable } from "../workoutHistoryTable";
import axiosInstance from "../../utils/axiosInstance";
import { AxiosResponse } from "axios/index";

type WorkoutHistoryProps = {
  id: string;
};

type HistoryElement = {
  id: string;
  date: string;
  trainingDetails: {
    exercise: {
      id: string;
      name: string;
      categoryName: string;
    };
    series: [
      {
        repeats: number;
        weight: number;
      },
    ];
  }[];
};

export const WorkoutHistoryPanel: React.FC<WorkoutHistoryProps> = ({ id }) => {
  const [training, setTraining] = useState<HistoryElement>();
  const [currentPage, setCurrentPage] = useState<number>(0);

  useEffect(() => {
    let token: string | null = localStorage.getItem("accessToken");
    let config2 = {
      headers: { Authorization: `Bearer ${token}` },
    };

    axiosInstance
      .get<HistoryElement>(`/api/trainings/${id}`, config2)
      .then((response: AxiosResponse<HistoryElement>) => {
        if (response.status === 200) {
          setTraining(response.data);
        }
      })
      .catch((error) => {
        console.log(error);
      });
  }, [id]);

  const handleNextPage = () => {
    setCurrentPage((prevPage) => prevPage + 2);
  };

  const handlePrevPage = () => {
    setCurrentPage((prevPage) => Math.max(0, prevPage - 2));
  };

  return (
    <div className="workout-history-page-content__panel">
      <div className="workout-history-page-content__panel-title">
        TWOJA HISTORIA
      </div>

      <div className="workout-history-page-content__panel-body">
        {training?.trainingDetails[currentPage] && (
          <WorkoutHistoryTable
            name={training?.trainingDetails[currentPage].exercise.name}
            array={training.trainingDetails[currentPage].series}
            textButton={"Poprzednia strona"}
            myFunction={handlePrevPage}
            currentPage={currentPage}
            maxPage={training?.trainingDetails.length}
          />
        )}

        {training?.trainingDetails[currentPage + 1] && (
          <WorkoutHistoryTable
            name={training?.trainingDetails[currentPage + 1].exercise.name}
            array={training.trainingDetails[currentPage + 1].series}
            textButton={"Następna strona"}
            myFunction={handleNextPage}
            currentPage={currentPage}
            maxPage={training?.trainingDetails.length}
          />
        )}
      </div>
    </div>
  );
};
