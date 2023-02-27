import Button from "../components/Button";
import React from "react";
import style from "../styles/Question.module.css";
import toastr from "toastr";
import "toastr/build/toastr.css";
import { useSelector, useDispatch } from "react-redux";
import * as Actions from "../redux/thousandQuestionsSlice"


function Question({ clearTimer }) {
  const state = useSelector(
    (state) => state.thousandQuestions.queue[state.thousandQuestions.index]
  );

  const { opacity, disabledButtons } = useSelector(
    (state) => state.thousandQuestions
  );

  const dispatch = useDispatch();

  const successMessages = [
    "Ileri boys go hear, se dem fit?",
    "Be like this year na our year o",
    "Damn!! You guys on fire",
    "Nice! Perfecto",
    "Daddy's proud",
    "They won't see GTCC coming this year",
    "Dem go collect this year",
  ];

  const failureMessages = [
    "Haba, even Wale knows it",
    "Smh!! We'll get there sha",
    "Hmmm...I smell 4th position this year",
    "Baptist boys are coming, just dey play",
    "Wahala! Wahala! Wahala!!!",
    "So sad",
  ];

  // const wrongAnswerProvided = () => (dispatch) => {
  //   try {
  //     dispatch(Actions.wrongAnswerAction());
  //   } catch (error) {
  //     console.log(error);
  //   }
  // };

  // const handleDisableButton = () => async(dispatch) => {
  //   try{
  //     dispatch(Actions.setDisableButtonAction(true))
  //   }
  //   catch(error){
  //     console.log(error);
  //   }
  // }

  const onFailClick = () => {
    // wrongAnswers();
    // dispatch(wrongAnswerProvided());

    clearTimer();

    dispatch(Actions.wrongAnswerAction());

    dispatch(Actions.setDisableButtonAction(true));

    toastr.options = {
      positionClass: "toast-top-full-width",
      progressBar: true,
      hideEasing: "linear",
      showMethod: "fadeIn",
      hideMethod: "fadeOut",
    };

    const randomNumber = Math.floor(Math.random() * 6);

    toastr.error(failureMessages[randomNumber], {
      closeButton: true,
      progressBar: true,
      timeOut: 2000,
      extendedTimeOut: 1000,
    });
  };

  // const correctAnswerProvided = () => async (dispatch) => {
  //   try {
  //     dispatch(Actions.correctAnswerAction());
  //   } catch (error) {
  //     console.log(error);
  //   }
  // };

  const onSuccessClick = () => {
    // correctAnswers();

    dispatch(Actions.correctAnswerAction());

    dispatch(Actions.setDisableButtonAction(true));

    clearTimer();

    toastr.options = {
      positionClass: "toast-top-full-width",
      progressBar: true,
      hideEasing: "linear",
      showMethod: "fadeIn",
      hideMethod: "fadeOut",
    };

    const randomNumber = Math.floor(Math.random() * 7);

    console.log(randomNumber);

    toastr.success(successMessages[randomNumber], {
      closeButton: true,
      progressBar: true,
      timeOut: 2000,
      extendedTimeOut: 1000,
    });
  };

  // const displayAction = () => async (dispatch) => {
  //   try {
  //     dispatch(Actions.setOpacityAction(1));
  //   } catch (error) {
  //     console.log(error);
  //   }
  // };

  const handleDisplayAnswer = () => {
    dispatch(Actions.setOpacityAction(1));
    clearTimer();
  };

  return (
    <div className={style.container}>
      <div className={style.question}>
        <h4 key={state?.id}>
          {state?.question}
          {/* Nigeria fought her civil war in what year, when did the war ened and
          who was president at that period ? */}
        </h4>
      </div>
      <div className={style.answer}>
        <p style={{ opacity: opacity }}>
          {state?.answer}
          {/* The civil war was fought in year ...., it ended in year.... and the
          president was President.... */}
        </p>
      </div>
      <div class={style.btnGroup}>
        <Button
          name="Display answer"
          click={handleDisplayAnswer}
          
        >
          <i class="fa fa-book" aria-hidden="true"></i>
        </Button>
        <Button
          name="Mark as correct"
          disabled={disabledButtons}
          click={onSuccessClick}
          backgroundColor={disabledButtons && "gray"}
          color={disabledButtons && "brown"}
        >
          <i class="fa fa-check" aria-hidden="true"></i>
        </Button>
        <Button
          name="Mark as wrong"
          disabled={disabledButtons}
          click={onFailClick}
          backgroundColor={disabledButtons && "gray"}
          color={disabledButtons && "brown"}
        >
          <i class="fa fa-times" aria-hidden="true"></i>
        </Button>
      </div>
    </div>
  );
}

export default Question;
