import React, { useEffect, useState } from "react";
import ButtonInfoHome from "../buttonInfoHome/buttonInfoHome";
import "./homeInfoGold.scss";


function HomeInfoGray(props) {
    return (
        <div className="home__gold-element">
            <div className="home__gold-element-text">
            {props.text}
            </div>
            <div className="home__gold-element-img">
                <img></img>
            </div>

        </div>
        
    );
}

export default HomeInfoGray;