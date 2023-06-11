namespace BibleQuiz.Core
{
    public class BibleService : IBibleService
    {
        /// <summary>
        /// The transient instance of IApiClient
        /// </summary>
        private readonly IApiClient _apiClient;

        public BibleService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        /// <summary>
        /// Method to get the readings from a bible passage
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiResponse<BibleApiVerseSearchResponse, object, object>> GetScripturesAsync(BibleVerseApiModel model)
        {
            var bibleVerse = await _apiClient.SendAsync<BibleVerseApiModel, BibleApiVerseSearchResponse, object, object>(
                new BibleApiRequest<BibleVerseApiModel>
                {
                    Uri = BibleApiRoutes.GetBibleVerseRoute(model.BibleVerse),
                    Method = HttpMethod.Get,
                });

            return bibleVerse;
        }

        /// <summary>
        /// Get the list of all the books
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResponse<BibleBookNamesResponse, object, object>> GetBiblelBookNames()
        {
            var books = await _apiClient.SendAsync<object, BibleBookNamesResponse, object, object>(new BibleApiRequest<object>
            {
                Uri = BibleApiRoutes.GetBibleBooksRoute,
                Method = HttpMethod.Get
            });

            return books;
        }
    }
}
