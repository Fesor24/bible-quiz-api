import React from "react";
import style from "../styles/Button.module.css";

function Button({ name, click, children, disabled, color, backgroundColor }) {
  return (
    <button class={style.btn} disabled ={disabled} onClick={click} style= {{color: color, backgroundColor:backgroundColor}}>
      {name} {children}
    </button>
  );
}

export default Button;
