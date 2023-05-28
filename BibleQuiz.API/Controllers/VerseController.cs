using BibleQuiz.Core;
using BibleQuiz.Core.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibleQuiz.API.Controllers
{
    [ApiController]
    public class VerseController : ControllerBase
    {
        /// <summary>
        /// The scoped instance of IUnitOfWork
        /// </summary>
        private readonly IUnitOfWork unit;

        public VerseController(IUnitOfWork unit)
        {
            this.unit = unit;
        }

        /// <summary>
        /// Endpoint to get random verse
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.FetchVerse)]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 86400)]
        public async Task<ApiResponse> GetVerseOfTheDay()
        {
            var spec = new VerseSpecification();

            var allVerses = await unit.Repository<VerseOfTheDayDataModel>().GetAllDataAsync(spec);

            // Get the length of all the verses
            if(allVerses.Count() == 0)
            {
                return new ApiResponse
                {
                    ErrorMessage = "No verse at the moment"
                };
            }

            // Get a random number
            var randomNumber = new Random().Next(allVerses.Count());

            // Get a random verse
            var randomVerse = allVerses[randomNumber];

            // Return the random verse
            return new ApiResponse
            {
                Result = randomVerse
            };
        }

        /// <summary>
        /// Endpoint to add verses
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost(ApiRoutes.AddVerses)]
        public async Task<ApiResponse> AddVerses(List<VerseOfTheDayApiModel> model)
        {
            // Create list of verse of the day api model
            List<VerseOfTheDayDataModel> verses = new();

            foreach(var verse in model)
            {
                var verseData = new VerseOfTheDayDataModel
                {
                    Book = verse.Book,
                    Passage = verse.Passage,
                };

                verses.Add(verseData);
            }

            await unit.Repository<VerseOfTheDayDataModel>().AddDataRange(verses);

            await unit.Complete();

            return new ApiResponse
            {
                Result = new { Message = "Verses added" }
            };
        }
    }
}
