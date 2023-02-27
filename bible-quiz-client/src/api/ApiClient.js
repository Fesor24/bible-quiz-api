import axios from "axios";
import ApiRoutes from "./ApiRoutes";
// import fs from 'browserify-fs';
// import https from "https-browserify";
// import https from "https"



// const httpsAgent = new https.Agent({
//     rejectUnauthorized: false
// })

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