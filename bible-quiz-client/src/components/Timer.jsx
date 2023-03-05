import style from "../styles/Timer.module.css";

function Timer({countdown}) {

  return (
    <div className={style.timer}>
      {/* <Circle borderWidth ={borderWidth} circleText= {"00:30"} colorText={colorText} /> */}

      <p>00:{countdown === 0 ? "00": countdown < 10 ? `0${countdown}` : countdown}</p>
    </div>
  );
}

export default Timer;
