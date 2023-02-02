using System.Net;
using BibleQuiz.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibleQuiz.API.Controllers
{
    
    [ApiController]
    public class QuizController : ControllerBase
    {
        #region Fields

        /// <summary>
        /// Scoped instance of dbContext
        /// </summary>
        private readonly ApplicationDbContext context;

        private readonly ILogger<QuizController> logger;

        #endregion

        #region Constructor
        public QuizController(ApplicationDbContext context, ILogger<QuizController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        #endregion

        /// <summary>
        /// End point to fetch a question from thousand quiz question
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet(ApiRoutes.FetchThousandQuestionById)]
        public async Task<ApiResponse> GetQuestionById(int id)
        {
            // Initialize thousand quiz data model
            var result = new ThousandQuizQuestionsDataModel();

            try
            {
                // Get the question
                var question = await context.ThousandQuizQuestions.FirstOrDefaultAsync(x => x.Id == id);

                // If question is null
                if (question is null) return NullResult();

                // Set the result
                result = question;

            }

            catch(Exception ex)
            {
                logger.LogError("An error occurred", ex.Message);

                return new ApiResponse
                {
                    ErrorMessage = ex.Message
                };
            }

            return new ApiResponse
            {
                Result = result
            };
        }

        private ApiResponse NullResult(string errorMessage = "Question not found")
        {
            // Set status code to not found
            Response.StatusCode = (int)HttpStatusCode.NotFound;

            // Return response
            return new ApiResponse
            {
                ErrorMessage = errorMessage,
            };

        }
    }
}
