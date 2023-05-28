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
        /// Endpoint to delete revision questions for a user
        /// </summary>
        public const string DeleteRevisionQuestionsForUser = "api/revision-question/delete";

        /// <summary>
        /// Endpoint to delete fesor question
        /// </summary>
        public const string DeleteQuestionById = "api/fesor-question/delete";

        /// <summary>
        /// Endpoint to add thousand questions to db
        /// </summary>
        public const string AddThousandQuestions = "api/thousand-questions/add";

		/// <summary>
		/// Endpoint to add thousand questions to db
		/// </summary>
		public const string AddFesorQuestions = "api/fesor-questions/add";

		/// <summary>
		/// Endpoint to fetch all revision questions
		/// </summary>
		public const string FetchRevisionQuestions = "api/revision-questions/fetch";

        /// <summary>
        /// Endpoint to fetch questions set by me
        /// </summary>
        public const string FetchFesorQuestions = "api/fesor-questions/fetch";

        /// <summary>
        /// Endpoint to register a user
        /// </summary>
        public const string Register = "api/register";

        /// <summary>
        /// Endpoint to login a user
        /// </summary>
        public const string Login = "api/login";

        /// <summary>
        /// Endpoint to get the current user
        /// </summary>
        public const string GetCurrentUser = "api/current-user";

        /// <summary>
        /// Endpoint to grant access to users
        /// </summary>
        public const string GrantAccess = "api/grant-access";

        /// <summary>
        /// Endpoint to fetch all users
        /// </summary>
        public const string FetchAllUsers = "api/fetch-users";

		/// <summary>
		/// Endpoint to fetch user
		/// </summary>
		public const string FetchUser = "api/fetch-user";

        /// <summary>
        /// Endpoint to Add verses
        /// </summary>
        public const string AddVerses = "api/verses";

        /// <summary>
        /// Endpoint to Add verses
        /// </summary>
        public const string FetchVerse = "api/verse";
    }
}
