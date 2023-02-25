import React, { useState } from "react";
import { Row, Col } from "antd";
import style from "../styles/ThousandQuestions.module.css";
import Sidebar from "../components/Sidebar";
import Timer from "../components/Timer";
import Question from "../components/Question";
import Button from "../components/Button";
import { Link } from "react-router-dom";

function ThousandQuestions() {
  const [correctAnswers, setCorrectAnswers] = useState(0);
  const [wrongAnswers, setWrongAsnwers] = useState(0);

  const increaseCorrectAnswers = () => {
    setCorrectAnswers(correctAnswers + 1);
  };

  const increaseWrongAnswers = () => {
    setWrongAsnwers(wrongAnswers + 1);
  };

  return (
    <Row>
      <Col span={18} push={6}>
        <div className={style.timer}>
          <Timer />
          <div>
            <div className={style.next}>
              <Button name={"Next"}>
                <i class="fa fa-arrow-right" aria-hidden="true"></i>
              </Button>
              <Link to="/">
                <p>Back to home</p>
              </Link>
            </div>
          </div>
        </div>

        <div className={style.question}>
          <Question
            correctAnswers={increaseCorrectAnswers}
            wrongAnswers={increaseWrongAnswers}
          />
        </div>
      </Col>

      <Col span={6} pull={18}>
        <Sidebar correct={correctAnswers} wrong={wrongAnswers} />
      </Col>
    </Row>
  );
}

export default ThousandQuestions;
