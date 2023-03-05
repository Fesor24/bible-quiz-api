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
        public const string FetchThousandQuestions = "api/thousand-questions/fetch";

        /// <summary>
        /// Endpoint to add a question to the revision table
        /// </summary>
        public const string AddRevisionQuestion = "api/revision-question/add";

        /// <summary>
        /// Endpoint to fetch all revision questions
        /// </summary>
        public const string FetchRevisionQuestions = "api/revision-questions/fetch";
    }
}
