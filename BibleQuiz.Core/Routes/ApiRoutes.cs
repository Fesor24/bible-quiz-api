namespace BibleQuiz.Core
{
    public static class ApiRoutes
    {
        /// <summary>
        /// Endpoint to fetch questions from thousand questions by id
        /// </summary>
        public const string FetchThousandQuestion = "api/thousand-question/{id}";

        /// <summary>
        /// Endpoint to fetch all questions from ThousandQuestions
        /// </summary>
        public const string FetchThousandQuestions = "api/get-all-questions";
    }
}
