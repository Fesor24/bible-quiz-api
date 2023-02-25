import React from "react";
import style from "../styles/Sidebar.module.css";

function Circle({ borderWidth, circleText, colorText }) {
  return (
    <div className={style.circle} style={borderWidth}>
      <p className={style.metric} style={colorText}>
        {circleText ?? 100}
      </p>
    </div>
  );
}

export default Circle;
