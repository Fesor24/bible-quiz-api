import axios from "axios";
import ApiRoutes from "./ApiRoutes";


 axios.defaults.baseURL = ApiRoutes.BASE_URL;

 // Hook to fetch thousand questions
 export function useFetchThousandQuestions(){
    async function fetchThousandQuestions(token) {
        var response = axios.get(ApiRoutes.FetchThousandQuestions, {
          headers: { Authorization: `Bearer ${token}` },
        });

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

 // Hook to fetch fesor questions
 export function useFetchFesorQuestions(){
    async function fetchFesorQuestions(){
        var response = axios.get(ApiRoutes.FetchFesorQuestions);

        return response;
    }

    return fetchFesorQuestions;
 }

 // Hook to login user
 export function useLoginUser(){
    async function loginUser(formData){
        var response = axios.post(ApiRoutes.LoginUser, formData)

        return response;
    }

    return loginUser;
 }

 // Hook to register a user
 export function useRegisterUser(){
    async function registerUser(formData){
        var response = axios.post(ApiRoutes.RegisterUser, formData)

        return response;
    }

    return registerUser;
 }