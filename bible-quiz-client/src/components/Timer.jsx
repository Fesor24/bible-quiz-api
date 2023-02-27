import style from "../styles/Timer.module.css";

function Timer({countdown}) {

  
  // const borderWidth = {
  //     border: "4px solid brown"
  // }

  // const colorText = {
  //     color: "brown"
  // }

  return (
    <div className={style.timer}>
      {/* <Circle borderWidth ={borderWidth} circleText= {"00:30"} colorText={colorText} /> */}

      <p>00:{countdown === 0 ? "00": countdown}</p>
    </div>
  );
}

export default Timer;
