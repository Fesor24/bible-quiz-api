import React, { useState, useEffect } from "react";
import { Row, Col } from "antd";
import style from "../styles/ThousandQuestions.module.css";
import Sidebar from "../components/Sidebar";
import Timer from "../components/Timer";
import Question from "../components/Question";
import Button from "../components/Button";
import { Link } from "react-router-dom";
import { useFetchThousandQuestions } from "../api/ApiClient";
import { useSelector, useDispatch } from "react-redux";
import * as Action from "../redux/thousandQuestionsSlice";
import toastr from "toastr";

function ThousandQuestions() {
  // const [correctAnswers, setCorrectAnswers] = useState(0);
  // const [wrongAnswers, setWrongAsnwers] = useState(0);
  const [disableButtons, setDisableButtons] = useState(false);
  const [thousandQuestions, setThousandQuestions] = useState();


  const questions = useSelector((state) => state.thousandQuestions.queue);
  const dispatch = useDispatch();

  const countdownNumber = 10;

  const { correctAnswers, wrongAnswers, questionsAttempted, index } = useSelector(
    (state) => state.thousandQuestions
  );

  const fetchThousandQuestions = useFetchThousandQuestions();

  useEffect(() => {
    async function fetchAllThousandQuestions() {
      await fetchThousandQuestions()
        .then((response) => {
          if (response.data.successful) {
            console.log(response.data.result);
            setThousandQuestions(response.data.result);
            dispatch(Action.startQuizAction(response.data.result));
          } else {
            console.log(response.data.errorMessage);
          }
        })
        .catch((error) => {
          console.log("error", error);
        });
    }

    if (!thousandQuestions) {
      fetchAllThousandQuestions();
    }
  }, [dispatch]);

  useEffect(() => {
    const correct = JSON.parse(localStorage.getItem("correctAnswer"));
    const wrong = JSON.parse(localStorage.getItem("wrongAnswer"));
    const index = JSON.parse(localStorage.getItem("questionsAttempted"));

    console.log(correct, wrong, index);

    if(index){
      dispatch(Action.setCorrectNumberAction(correct));
      dispatch(Action.setWrongNumberAction(wrong));
      dispatch(Action.setIndexNumberAction(index));
      dispatch(Action.setQuestionsAttemptedAction(index));
    }
  }, []);


  // Countdown state
  const [countdown, setCountdown] = useState(countdownNumber);
  const [finishedTimer, setFinishedTimer] = useState(true);

  let timerId;

  // Setting timer function in use effect
  useEffect(() => {
    if (countdown > 0) {
      timerId = setTimeout(() => {
        setCountdown(countdown - 1);
      }, 1000);

      setFinishedTimer(true);
      return () => clearTimeout(timerId);
    } else {
      setFinishedTimer(false);
      setDisableButtons(true);
      dispatch(Action.setOpacityAction(1));
      dispatch(Action.setDisableButtonAction(true));
      dispatch(Action.wrongAnswerAction());
      // increaseWrongAnswers();
      // console.log(state);
    }
  }, [countdown]);

  const clearTimer = () => {
    clearTimeout(timerId);
  }

  // const nextQuestion = () => async (dispatch) => {
  //   try {
  //     dispatch(Action.nextQuestionAction());
  //   } catch (error) {
  //     console.log(error);
  //   }
  // };

  // const remainingQuestion = () => async (dispatch) => {
  //   try{
  //     dispatch(Action.remainingQuestionsAction());
  //   }
  //   catch(error){
  //     console.log(error)
  //   }
  // }

  // const resetHideAnswer = () => async (dispatch) => {
  //   try{
  //     dispatch(Action.setOpacityAction(0));
  //   }
  //   catch(error){
  //     console.log(error);
  //   }
  // }

  // const resetDisableButton = () => async (dispatch) => {
  //   try{
  //     dispatch(Action.setDisableButtonAction(false));
  //   }
  //   catch(error){
  //     console.log(error);
  //   }
  // }

  const handleNextButtonClick = () => {
    // setFinishedTimer(true);
    setCountdown(countdownNumber);
    setDisableButtons(false);
    dispatch(Action.nextQuestionAction());
    dispatch(Action.remainingQuestionsAction());
    dispatch(Action.setOpacityAction(0));
    dispatch(Action.setDisableButtonAction(false));
    console.log(index);
    // setAttemptedQuestions(attemptedQuestions + 1);
  };

  const handleSaveButtonClick = () => {
    localStorage.setItem("correctAnswer", JSON.stringify(correctAnswers));
    localStorage.setItem("wrongAnswer", JSON.stringify(wrongAnswers));
    localStorage.setItem("questionsAttempted", JSON.stringify(questionsAttempted));
    toastr.success("Saved")
  }

  // const resetQuiz = () => async(dispatch) => {
  //   dispatch(Action.resetIndexAction());
  // }

  const handleResetButtonClick = () => {
    localStorage.clear();
    dispatch(Action.resetIndexAction());
    setCountdown(countdownNumber);
    dispatch(Action.setOpacityAction(0));
    dispatch(Action.setDisableButtonAction(false));
  }

  return (
    <Row>
      <Col span={18} push={6}>
        <div className={style.timer}>
          <Timer countdown={countdown} />
          <div>
            <div className={style.next}>
              <Button click={handleNextButtonClick}>
                <i class="fa fa-arrow-right" aria-hidden="true"></i>
              </Button>
              &nbsp; &nbsp;
              <Button click={handleSaveButtonClick}>
                <i class="fa-regular fa-floppy-disk"></i>
              </Button>
              &nbsp; &nbsp;
              <Button click={handleResetButtonClick}>
                <i class="fa-solid fa-ban"></i>
              </Button>
              &nbsp; &nbsp;
              <Link to="/category">
                <Button>
                  <i class="fa fa-arrow-left" aria-hidden="true"></i>
                </Button>
              </Link>
            </div>
          </div>
        </div>

        <div className={style.question}>
          <Question
            disableButtons={disableButtons}
            // correctAnswers={increaseCorrectAnswers}
            // wrongAnswers={increaseWrongAnswers}
            displayAnswer={finishedTimer}
            clearTimer = {clearTimer}
          />
        </div>
      </Col>

      <Col span={6} pull={18}>
        <Sidebar
          correct={correctAnswers}
          wrong={wrongAnswers}
          remaining={questions?.length - questionsAttempted}
          total={questions?.length}
        />
      </Col>
    </Row>
  );
}

export default ThousandQuestions;
