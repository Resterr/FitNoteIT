import React, { useState } from "react";
import "./loginForm.scss"



function LoginForm() {

    return (
        <div className="login__form">
            <form>
                <div className="login__form-login">
                    <label htmlFor="fname">Login:</label><br/>
                    <input type="text" id="fname" name="fname" />
                </div>
                <div className="login__form-password">
                    <label htmlFor="lname">Hasło:</label><br/>
                    <input type="password" id="lname" name="lname" /><br/>
                    
                </div>
                <div className="login__form-button">
                         <button type="submit">Zaloguj</button>
                </div>
            </form>
        </div>
    );
}

export default LoginForm;