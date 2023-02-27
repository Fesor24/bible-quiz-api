import React from "react";
import style from "../styles/Button.module.css";

function Button({ name, click, children, disabled }) {
  return (
    <button class={style.btn} disabled ={disabled} onClick={click}>
      {name} {children}
    </button>
  );
}

export default Button;
