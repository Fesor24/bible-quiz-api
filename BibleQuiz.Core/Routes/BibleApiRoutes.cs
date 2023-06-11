namespace BibleQuiz.Core
{
    public static class BibleApiRoutes
    {
        private static string _baseUrl = "https://api.scripture.api.bible/v1/bibles/";

        /// <summary>
        /// Method to get bible verses
        /// verses are in this format -- JHN.1.1-2
        /// </summary>
        /// <param name="verse"></param>
        /// <returns></returns>
        public static string GetBibleVerseRoute(string verse)
        {
            return _baseUrl + $"de4e12af7f28f599-01/search?query={verse}";
        }

        /// <summary>
        /// Get Bible books route
        /// </summary>
        public static string GetBibleBooksRoute => _baseUrl + "de4e12af7f28f599-01/books";
    }
}
