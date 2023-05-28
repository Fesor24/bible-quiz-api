using System.Net;
using BibleQuiz.Core;
using BibleQuiz.Core.Specification;
using Microsoft.AspNetCore.Authorization;
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
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 3600)]
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
        /// Endpoint to delete a verse
        /// </summary>
        /// <param name="verseId"></param>
        /// <returns></returns>
        [HttpDelete(ApiRoutes.DeleteVerse)]
        [Authorize(Policy = "RequireAdminClaim")]
        public async Task<ApiResponse> DeleteVerse([FromQuery] int verseId)
        {
            // Create new spec
            var spec = new VerseSpecification(verseId);

            // Get the verse
            var verse = await unit.Repository<VerseOfTheDayDataModel>().GetDataWithSpec(spec);

            // If verse is null
            if (verse is null)
            {
                // Set the status code
                Response.StatusCode = (int)HttpStatusCode.NotFound;

                return new ApiResponse
                {
                    ErrorMessage = "Verse not found"
                };
            }

            // Delete the verse
            unit.Repository<VerseOfTheDayDataModel>().DeleteData(verse);

            // Save changes
            await unit.Complete();

            // Return the response
            return new ApiResponse
            {
                Result = new { Message = "Verse deleted" }
            };
        }

        /// <summary>
        /// Endpoint to fetch all verses
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.FetchVerses)]
        [Authorize(Policy = "RequireAdminClaim")]
        public async Task<ApiResponse> FetchAllVerses()
        {
            // Create a new spec
            var spec = new VerseSpecification();

            // Get all the verses
            var verses = await unit.Repository<VerseOfTheDayDataModel>().GetAllDataAsync(spec);

            // Return it
            return new ApiResponse
            {
                Result = verses
            };
        }     

        /// <summary>
        /// Endpoint to add verses
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost(ApiRoutes.AddVerses)]
        [Authorize(Policy = "RequireAdminClaim")]
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
