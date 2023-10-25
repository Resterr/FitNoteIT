import React from "react";
import DatePicker, { ReactDatePickerProps } from "react-datepicker";

interface CustomDatePickerProps extends ReactDatePickerProps {
  exerciseName: string;
}

export const CustomDatePicker: React.FC<CustomDatePickerProps> = (props) => {
  const { exerciseName, ...otherProps } = props;

  return <DatePicker {...otherProps} />;
};
