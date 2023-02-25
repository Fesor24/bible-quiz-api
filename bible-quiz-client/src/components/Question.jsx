import Button from "../components/Button";
import React from "react";
import style from "../styles/Question.module.css";
import toastr from "toastr";
import "toastr/build/toastr.css";

function Question({ correctAnswers, wrongAnswers }) {
  const successMessages = [
    "Ileri boys go hear, se dem fit?",
    "Be like this year na our year o",
    "Damn!! You guys on fire",
    "Nice! Perfecto",
    "Daddy's proud",
    "They won't see GTCC coming this year",
  ];

  const failureMessages = [
    "Haba, even Wale knows it",
    "Smh!! We'll get there sha",
    "Hmmm...I smell 4th position this year",
    "Baptist boys are coming, just dey play",
    "Wahala! Wahala! Wahala!!!",
    "I'm ashamed",
  ];

  const onFailClick = () => {
    wrongAnswers();

    toastr.options = {
      positionClass: "toast-top-full-width",
      progressBar: true,
      hideEasing: "linear",
      showMethod: "fadeIn",
      hideMethod: "fadeOut",
    };

    const randomNumber = Math.floor(Math.random() * 5);

    toastr.error(failureMessages[randomNumber], {
      closeButton: true,
      progressBar: true,
      timeOut: 5000,
      extendedTimeOut: 2000,
    });
  };

  const onSuccessClick = () => {
    correctAnswers();
    toastr.options = {
      positionClass: "toast-top-full-width",
      progressBar: true,
      hideEasing: "linear",
      showMethod: "fadeIn",
      hideMethod: "fadeOut",
    };

    const randomNumber = Math.floor(Math.random() * 5);

    console.log(randomNumber);

    toastr.success(successMessages[randomNumber], {
      closeButton: true,
      progressBar: true,
      timeOut: 5000,
      extendedTimeOut: 2000,
    });
  };

  return (
    <div className={style.container}>
      <div className={style.question}>
        <h4>
          Nigeria fought her civil war in what year, when did the war ened and
          who was president at that period ?
        </h4>
      </div>
      <div className={style.answer}>
        <p>
          The civil war was fought in year ...., it ended in year.... and the
          president was President....
        </p>
      </div>
      <div class={style.btnGroup}>
        <Button name="Display answer">
          <i class="fa fa-book" aria-hidden="true"></i>
        </Button>
        <Button name="Mark as correct" click={onSuccessClick}>
          <i class="fa fa-check" aria-hidden="true"></i>
        </Button>
        <Button name="Mark as wrong" click={onFailClick}>
          <i class="fa fa-times" aria-hidden="true"></i>
        </Button>
      </div>
    </div>
  );
}

export default Question;
