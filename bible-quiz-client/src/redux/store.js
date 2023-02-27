import { configureStore, combineReducers } from "@reduxjs/toolkit";
import questionReducer from "./thousandQuestionsSlice";

const rootReducer = combineReducers({
  thousandQuestions: questionReducer,
});

export default configureStore({ reducer: rootReducer });
