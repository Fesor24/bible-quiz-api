using System.Net;
using BibleQuiz.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
		[Authorize(Policy = "RequirePremiumClaim")]
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
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "RequirePremiumClaim")]
		public async Task<ApiResponse> FetchAllThousandQuestions()
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
        /// Endpoint to add question to the revision table
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost(ApiRoutes.AddRevisionQuestion)]
        public async Task<ApiResponse> AddRevisionQuestion(RevisionQuestionApiModel model)
        {
            // Check if the question exist
            var questionExist = await context.RevisionQuestions.AnyAsync(x => x.Question.ToLower() == model.Question.ToLower());

            // If the question exist
            if (questionExist)
            {
                // Return message to client
                return new ApiResponse
                {
                    ErrorMessage = "Question already in table"
                };
            }

            // Add the question to db
            await unit.Repository<RevisionQuestionsDataModel>().AddQuestions(new RevisionQuestionsDataModel
            {
                Question = model.Question,
                Answer = model.Answer
            });

            // Save the changes
            await unit.Complete();

            // Return the api response
            return new ApiResponse
            {
                Result = "Question successfully added"
            };
        }

        /// <summary>
        /// Endpoint to fetch all revision questions
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.FetchRevisionQuestions)]
		[Authorize(Policy = "RequirePremiumClaim")]
		public async Task<ApiResponse> FetchRevisionQuestions()
        {
            // We create an instance of revision specification
            var spec = new RevisionQuestionsSpecification();

            // Fetch all the questions
            var questions = await unit.Repository<RevisionQuestionsDataModel>().GetQuestionsAsync(spec);

            // Return questions to client
            return new ApiResponse
            {
                Result = questions
            };
        }

        /// <summary>
        /// Fetch questions set by me
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.FetchFesorQuestions)]
        [Authorize(Policy = "RequirePremiumClaim")]
        public async Task<ApiResponse> FetchFesorsQuestions()
        {
            // Create an instance of the fesor specification
            var spec = new FesorQuestionsSpecification();

            // Fetch all questions
            var questions = await unit.Repository<FesorQuestionsDataModel>().GetQuestionsAsync(spec);

            // Return the questions
            return new ApiResponse
            {
                Result = questions
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
