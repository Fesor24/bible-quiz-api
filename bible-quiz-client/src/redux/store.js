import { configureStore, combineReducers } from "@reduxjs/toolkit";
import questionReducer from "./thousandQuestionsSlice";
import revisionReducer from "./revisionQuestionSlice"

const rootReducer = combineReducers({
  thousandQuestions: questionReducer,
  revisionQuestions: revisionReducer
});

export default configureStore({ reducer: rootReducer });
