import { createSlice } from "@reduxjs/toolkit"

const questionSlice = createSlice({
    name: 'thousand-questions',
    initialState: {
        queue : [],
        index: 0,
        correctAnswers: 0,
        wrongAnswers: 0,
        questionsAttempted: 0
    },
    reducers: {
        startQuizAction : (state, action) => {
            return {
                ...state,
                queue: action.payload
            }
        },
        nextQuestionAction : (state) => {
            return{
                ...state,
                index: state.index + 1
            }
        },
        correctAnswerAction: (state) => {
            return {
                ...state,
                correctAnswers: state.correctAnswers + 1
            }
        },
        wrongAnswerAction: (state) => {
            return{
                ...state,
                wrongAnswers: state.wrongAnswers + 1
            }
        },
        remainingQuestionsAction: (state) => {
            return {
              ...state,
              questionsAttempted: state.questionsAttempted + 1,
            };
        },
        setCorrectNumberAction: (state, action) => {
            return{
                ...state,
                correctAnswers: action.payload
            }
        },
        setWrongNumberAction: (state, action) => {
            return{
                ...state,
                wrongAnswers: action.payload
            }
        },

        setIndexNumberAction: (state, action) => {
            return {
                ...state,
                trace: action.payload
            }
        },
        setQuestionsAttemptedAction: (state, action) => {
            return {
              ...state,
              questionsAttempted: action.payload
            };
        }
    }
})

export const { startQuizAction, nextQuestionAction, correctAnswerAction, wrongAnswerAction,
     remainingQuestionsAction, setCorrectNumberAction, setWrongNumberAction, setQuestionsAttemptedAction,
    setIndexNumberAction} = questionSlice.actions;

export default questionSlice.reducer;