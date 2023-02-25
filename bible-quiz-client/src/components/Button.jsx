import React from "react";
import style from "../styles/Button.module.css";

function Button({ name, click, children }) {
  return (
    <button class={style.btn} onClick={click}>
      {name} {children}
    </button>
  );
}

export default Button;
