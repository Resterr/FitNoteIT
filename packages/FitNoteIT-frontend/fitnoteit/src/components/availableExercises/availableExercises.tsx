import React from "react";
import "./availableExercises.scss";
import NavigateNextIcon from "@mui/icons-material/NavigateNext";
import NavigateBeforeIcon from "@mui/icons-material/NavigateBefore";
import ControlPointIcon from "@mui/icons-material/ControlPoint";

type Exercise = {
  name: string;
  categoryName: string;
  id: number;
};

type AvailableExercisesProps = {
  exercises: Exercise[];
  categories: string[];
  addHandle: (exercise: Exercise) => void;
};

export const AvailableExercises: React.FC<AvailableExercisesProps> = ({
  exercises,
  categories,
  addHandle,
}) => {
  const exercisesByCategory: Record<string, Exercise[]> = {};

  exercises.forEach((exercise) => {
    if (!exercisesByCategory[exercise.categoryName]) {
      exercisesByCategory[exercise.categoryName] = [];
    }
    exercisesByCategory[exercise.categoryName].push(exercise);
  });

  const [selectedCategoryIndex, setSelectedCategoryIndex] = React.useState(0);

  const changeCategory = (delta: number) => {
    const newIndex = selectedCategoryIndex + delta;
    if (newIndex >= 0 && newIndex < categories.length) {
      setSelectedCategoryIndex(newIndex);
    }
  };

  const selectedCategoryExercises =
    exercisesByCategory[categories[selectedCategoryIndex]];

  return (
    <div className="add-plan-page-content__available-exercises">
      <div className="add-plan-page-content__available-exercises-title">
        <h1>DOSTĘPNE ĆWICZENIA</h1>
      </div>
      <div className="add-plan-page-content__available-exercises-category">
        <span
          className="add-plan-page-content__available-exercises-category-before"
          onClick={() => changeCategory(-1)}
        >
          <NavigateBeforeIcon />
        </span>
        <span>{categories[selectedCategoryIndex]}</span>
        <span
          className="add-plan-page-content__available-exercises-category-next"
          onClick={() => changeCategory(1)}
        >
          <NavigateNextIcon />
        </span>
      </div>
      <div className="add-plan-page-content__available-exercises-list">
        <ul>
          {selectedCategoryExercises ? (
            selectedCategoryExercises.map((exercise) => (
              <li
                key={exercise.id}
                className="add-plan-page-content__available-exercises-list-element"
              >
                {exercise.name}
                <span className="add-plan-page-content__available-exercises-list-element-plus">
                  <ControlPointIcon onClick={() => addHandle(exercise)} />
                </span>
              </li>
            ))
          ) : (
            <li key="no-exercises">No exercises available in this category</li>
          )}
        </ul>
      </div>
    </div>
  );
};
