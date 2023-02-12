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

        /// <summary>
        /// DI instance of ILogger
        /// </summary>
        private readonly ILogger<QuizController> logger;

        /// <summary>
        /// Scoped instance of Generic Repository
        /// </summary>
        private readonly IGenericRepository<ThousandQuizQuestionsDataModel> thousandQuiz;

        #endregion

        #region Constructor
        public QuizController(ApplicationDbContext context, ILogger<QuizController> logger, IGenericRepository<ThousandQuizQuestionsDataModel> thousandQuiz)
        {
            this.context = context;
            this.logger = logger;
            this.thousandQuiz = thousandQuiz;
        }

        #endregion

        /// <summary>
        /// End point to fetch a question from thousand quiz question
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet(ApiRoutes.FetchThousandQuestion)]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, type: typeof(ApiResponse))]
        [ProducesResponseType(statusCode:(int)HttpStatusCode.NotFound, type:typeof(ApiResponse))]
        public async Task<ApiResponse> GetQuestionById(int id)
        {
            // Initialize thousand quiz data model
            var result = new ThousandQuizQuestionsDataModel();

            try
            {
                // Get the question
                var question = await thousandQuiz.GetQuestionByIdAsync(id);              

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

        /// <summary>
        /// Endpoint to fetch all questions from ThousandQuestions
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.FetchThousandQuestions)]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, type: typeof(ApiResponse))]
        public async Task<ApiResponse> GetAllQuestions()
        {
            // Fetch all the questions
            var result = await thousandQuiz.GetAllQuestionsAsync();

            // return it to client
            return new ApiResponse
            {
                Result = result
            };
        }

        /// <summary>
        /// Private class to handle null result
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
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
