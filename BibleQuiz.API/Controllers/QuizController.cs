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
        /// Scoped instance of IUnitOfWork
        /// </summary>
        private readonly IUnitOfWork unit;

        #endregion

        #region Constructor
        public QuizController(ApplicationDbContext context, ILogger<QuizController> logger,
            IUnitOfWork unit)
        {
            this.context = context;
            this.logger = logger;
            this.unit = unit;
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
                // Create the specification
                var spec = new ThousandQuestionsSpecification(id);

                // Get the question
                var question = await unit.Repository<ThousandQuizQuestionsDataModel>().GetQuestionWithSpec(spec);            

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
            // Initialize new spec
            var spec = new ThousandQuestionsSpecification();

            // Fetch all the questions
            var result = await unit.Repository<ThousandQuizQuestionsDataModel>().GetQuestionsAsync(spec);

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
