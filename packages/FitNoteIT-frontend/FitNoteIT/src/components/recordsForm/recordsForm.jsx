import React, { useState, useEffect } from "react";
import "./recordsForm.scss"
import { useForm } from 'react-hook-form';
import axios from "axios";
import { useNavigate } from 'react-router-dom';
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";

function RecordsForm() {
    const currentUser = localStorage.getItem("currentUser");
    let navigate = useNavigate();
    const { register, handleSubmit, watch, formState: { errors } } = useForm();
    const [startDate, setStartDate] = React.useState(new Date());

    //  useEffect(() => {
    //     if (!currentUser) {
    //       navigate('/');
    //       } 
    //   }, []);


    const onSubmit = async data => {
        console.log(data)
        data["date"] = startDate;
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
                        <select {...register('exercise', { required: "This is required" })}>
                            <option value="">--Wybierz cwiczenie--</option>
                            <option value="push-ups">Pompki</option>
                            <option value="squats">Przysiady</option>
                            <option value="burpees">Burpees</option>
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
                            id="form-id"
                            type="text"
                            {...register('weight', {
                                required: "This is required",
                                pattern: /^[0-9\b]+$/
                            })}
                        /><br />
                        <p>{errors.weight?.message}</p>
                    </div>

                    <div className="records__form-button">
                        <button type="submit">Zapisz</button>
                    </div>
                </form>
            </div>
        </div>
    );
}

export default RecordsForm;