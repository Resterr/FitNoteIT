import React, { useState, useEffect } from "react";
import "./recordsForm.scss"
import { useForm } from 'react-hook-form';
import axios from "axios";
import { useNavigate } from 'react-router-dom';
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import ModalRecordsX from "../modalRecordsX/modalRecordsX";

function RecordsForm() {
    const currentUser = localStorage.getItem("currentUser");
    let navigate = useNavigate();
    const { register, handleSubmit, watch, formState: { errors } } = useForm();
    const [startDate, setStartDate] = React.useState(new Date());
    const [startWeight, setStartWeight] = React.useState(0);
    const [selectedValue, setSelectedValue] = useState("Martwy Ciąg");

    

     useEffect(() => {
        if (!currentUser) {
          navigate('/');
          } 
      }, []);


      const [exercises, setExercises] = useState(
        [
            {
                name: "Martwy Ciąg",
                recordDate:  new Date(2022, 2, 1),
                result: 12
            },
            {
                name: "Pompki",
                recordDate:  new Date(2022, 2, 3),
                result: 2
            },
            {
                name: "Przysiad",
                recordDate:  new Date(2022, 2, 3),
                result: 3
            },
            {
                name: "Wiosłowanie",
                recordDate:  new Date(2022, 2, 3),
                result: 4
            },
            {
                name: "Podciąganie",
                recordDate:  new Date(2022, 2, 3),
                result: 5
            },
            {
                name: "Rwanie",
                recordDate:  new Date(2022, 2, 3),
                result: 6
            },
            {
                name: "Uginanie na biceps",
                recordDate:  new Date(2022, 2, 3),
                result: 7
            },
            {
                name: "Dipy",
                recordDate:  new Date(2022, 2, 3),
                result: 8
            },
            {
                name: "OHP",
                recordDate:  new Date(2022, 2, 3),
                result: 9
            },
            {
                name: "Wyciskanie na ławce",
                recordDate:  new Date(2022, 2, 3),
                result: 10
            },
        ]
      );

      useEffect(() => {
        const selectedExerciseData = exercises.find(exercise => exercise.name === selectedValue);
        setStartDate(selectedExerciseData.recordDate);
        let inp = document.getElementById("weight")
        inp.value=selectedExerciseData.result
        setStartWeight(selectedExerciseData.result);
    }, [selectedValue]);

   


    const onSubmit = async data => {
        console.log(data)
        data["date"] = startDate;
        data["exercise"] = selectedValue;
        data["weight"] = startWeight;
        // await axios
        //  .post(
        //      'https://fitnoteit.azurewebsites.net/users/register',
        //      data
        //   )
        //  .then(response => {
        //     if(response.status==200){
        //         setStatus("Udało się zarejestrować")
        //         document.getElementById('form-id').value="";
        //         document.getElementById('form-login').value="";
        //         document.getElementById('form-password').value="";
        //         document.getElementById('form-password2').value="";
        //     }
        //     else {
        //         setStatus("Nie udało się zarejestrować")
        //     } 
        // });
    };
    return (
        <div className="records__form">
            <div className="records__form-all">
                <div className="records__form-title"><h1>Twoje rekordy</h1></div>
                <form onSubmit={handleSubmit(onSubmit)} id="my-form">
                    <div className="records__form-exercise">
                        <label htmlFor="exercise">Cwiczenie:</label><br />
                        <select value={selectedValue} onChange={(v) => setSelectedValue(v.target.value)} >
                            <option value="Martwy Ciąg">Martwy Ciąg</option>
                            <option value="Pompki">Pompki</option>
                            <option value="Przysiad">Przysiad</option>
                            <option value="Wiosłowanie">Wiosłowanie</option>
                            <option value="Podciąganie">Podciąganie</option>
                            <option value="Rwanie">Rwanie</option>
                            <option value="Uginanie na biceps">Uginanie na biceps</option>
                            <option value="Dipy">Dipy</option>
                            <option value="OHP">OHP</option>
                            <option value="Wyciskanie na ławce">Wyciskanie na ławce</option>       
                        </select>
                        <p>{errors.exercise?.message}</p>
                    </div>
                    <div className="records__form-date">
                        <label htmlFor="date">Data:</label><br />
                        <DatePicker
                            selected={startDate}
                            onChange={(date) => setStartDate(date)}
                            dateFormat="dd/MM/yyyy"
                            name="date"
                        /><br />
                        <p>{errors.date?.message}</p>
                    </div>
                    <div className="records__form-weight">
                        <label htmlFor="weight">Ciężar/Powtórzenia:</label><br />
                        <input
                            id="weight"
                            type="text"  
                            onChange={(v) => setStartWeight(v.target.value)}
                        /><br />
                        <p>{errors.weight?.message}</p>
                    </div>

                    <div className="records__form-button">
                        <button type="submit">Zapisz</button>
                        <ModalRecordsX></ModalRecordsX>
                    </div>
                </form>
            </div>
        </div>
    );
}

export default RecordsForm;