import axios from "axios";
import ApiRoutes from "./ApiRoutes";


 axios.defaults.baseURL = ApiRoutes.BASE_URL;

 // Hook to fetch thousand questions
 export function useFetchThousandQuestions(){
    async function fetchThousandQuestions() {
        var response = axios.get(
         ApiRoutes.FetchThousandQuestions
        );

        return response;
    }

    return fetchThousandQuestions;
 }

 // Hook to add question to revision table
 export function useAddRevisionQuestion(){
    async function addRevisionQuestion(question){
        var response = axios.post(
            ApiRoutes.AddRevisionQuestion, question
        )

        return response;
    }

    return addRevisionQuestion;
 }

 // Hook to fetch revision questions
 export function useFetchRevisionQuestions(){
    async function fetchRevisionQuestions(){
        var response = axios.get(
            ApiRoutes.FetchRevisionQuestions
        )
        return response;
    }

    return fetchRevisionQuestions;
 }