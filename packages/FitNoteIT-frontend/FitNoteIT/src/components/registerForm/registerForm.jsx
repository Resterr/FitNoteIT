import React, { useState } from "react";
import "./registerForm.scss"



function RegisterForm() {

    return (
        <div className="register__form">
            <form>
                <div className="register__form-login">
                    <label htmlFor="fname">Login:</label><br/>
                    <input type="text" id="fname" name="fname" />
                </div>
                <div className="register__form-password">
                    <label htmlFor="lname">Hasło:</label><br/>
                    <input type="password" id="lname" name="lname" /><br/>
                </div>
                <div className="register__form-password-again">
                    <label htmlFor="lname2"> Powtórz Hasło:</label><br/>
                    <input type="password" id="lname2" name="lname2" /><br/>
                </div>
                <div className="register__form-email">
                    <label htmlFor="email"> Powtórz Hasło:</label><br/>
                    <input type="email" id="email" name="email" /><br/>
                </div>
                <div className="register__form-button">
                         <button type="submit">Zarejestruj</button>
                </div>
            </form>
        </div>
    );
}

export default RegisterForm;