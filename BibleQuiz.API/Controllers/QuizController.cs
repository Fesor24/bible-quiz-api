using System.Net;
using System.Security.Claims;
using BibleQuiz.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        /// DI instance of UserManager
        /// </summary>

        private readonly UserManager<ApplicationUser> userManager;

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
            IUnitOfWork unit, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.logger = logger;
            this.unit = unit;
            this.userManager = userManager;
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
        //[Authorize(Policy = "RequirePremiumClaim")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ApiResponse> GetQuestionById(int id)
        {
            // Initialize thousand quiz data model
            var result = new ThousandQuizQuestionsDataModel();

            try
            {
                // Create the specification
                var spec = new ThousandQuestionsSpecification(id);

                // Get the question
                var question = await unit.Repository<ThousandQuizQuestionsDataModel>().GetDataWithSpec(spec);            

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
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "RequirePremiumClaim")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 5000)]
        public async Task<ApiResponse> FetchAllThousandQuestions()
        {
            // Initialize new spec
            var spec = new ThousandQuestionsSpecification();

            // Fetch all the questions
            var result = await unit.Repository<ThousandQuizQuestionsDataModel>().GetAllDataAsync(spec);

            // return it to client
            return new ApiResponse
            {
                Result = result
            };
        }

        /// <summary>
        /// Endpoint to add a list of questions to the thousand questions db
        /// </summary>
        /// <param name="questions"></param>
        /// <returns></returns>
        [HttpPost(ApiRoutes.AddThousandQuestions)]
		[Authorize(Policy = "RequireAdminClaim")]
		public async Task<ApiResponse> AddThousandQuestions(List<CreateQuestionsApiModel> questions)
        {
            // Create list of Thousand quiz questions
            List<ThousandQuizQuestionsDataModel> questionList = new();

            // Iterate over the loop
            foreach(var question in questions)
            {
                // Create instance of thousand quiz question data model
                var questionToAdd = new ThousandQuizQuestionsDataModel { Question = question.Question, Answer = question.Answer };

                // Add it to the question list
                questionList.Add(questionToAdd);
            }

            // Add it to the db
            await unit.Repository<ThousandQuizQuestionsDataModel>().AddDataRange(questionList);

            // Save the changes
            await unit.Complete();

            // Return response
            return new ApiResponse { Result = "Questions added to db" };
        }

        /// <summary>
        /// Endpoint to add questions to fesor's db
        /// </summary>
        /// <param name="questions"></param>
        /// <returns></returns>
		[HttpPost(ApiRoutes.AddFesorQuestions)]
		[Authorize(Policy = "RequireAdminClaim")]
		public async Task<ApiResponse> AddFesorQuestions(List<CreateQuestionsApiModel> questions)
		{
			// Create list of Thousand quiz questions
			List<FesorQuestionsDataModel> questionList = new();

			// Iterate over the loop
			foreach (var question in questions)
			{
				// Create instance of thousand quiz question data model
				var questionToAdd = new FesorQuestionsDataModel { Question = question.Question, Answer = question.Answer };

				// Add it to the question list
				questionList.Add(questionToAdd);
			}

			// Add it to the db
			await unit.Repository<FesorQuestionsDataModel>().AddDataRange(questionList);

			// Save the changes
			await unit.Complete();

			// Return response
			return new ApiResponse { Result = "Questions added to db" };
		}

		/// <summary>
		/// Endpoint to add question to the revision table
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost(ApiRoutes.AddRevisionQuestion)]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<ApiResponse> AddRevisionQuestion(RevisionQuestionApiModel model)
        {
            // Fetch the user email
            var userEmail = HttpContext.User.FindFirstValue(ClaimTypes.Email);

            // Fetch the user
            var user = await userManager.FindByEmailAsync(userEmail);

            // If the user is null
            if(user is null)
            {
                // Return error response
                return new ApiResponse
                {
                    ErrorMessage = "User not found"
                };
            }

            // Check if the question exist for the particular user
            var questionExist = await context.RevisionQuestions.AnyAsync(x => x.Question.ToLower() == model.Question.ToLower() 
            && x.UserId == user.Id);

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
            await unit.Repository<RevisionQuestionsDataModel>().AddData(new RevisionQuestionsDataModel
            {
                Question = model.Question,
                Answer = model.Answer,
                UserId = user.Id
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
        //[Authorize(Policy = "RequirePremiumClaim")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ApiResponse> FetchRevisionQuestions()
        {
            //Get the user mail
            var userMail = HttpContext.User.FindFirstValue(ClaimTypes.Email);

            // Get the user
            var user = await userManager.FindByEmailAsync(userMail);

            // If the user is null
            if (user is null)
                return new ApiResponse { ErrorMessage = "User not found" };

            // We create an instance of revision specification
            var spec = new RevisionQuestionsSpecification(user.Id);

            // Fetch all the questions
            var questions = await unit.Repository<RevisionQuestionsDataModel>().GetAllDataAsync(spec);

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
        //[Authorize(Policy = "RequirePremiumClaim")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 5000)]
        public async Task<ApiResponse> FetchFesorsQuestions()
        {
            // Create an instance of the fesor specification
            var spec = new FesorQuestionsSpecification();

            // Fetch all questions
            var questions = await unit.Repository<FesorQuestionsDataModel>().GetAllDataAsync(spec);

            // Return the questions
            return new ApiResponse
            {
                Result = questions
            };

        }

        /// <summary>
        /// Endpoint to delete revision questions for a user
        /// </summary>
        /// <returns></returns>
        [HttpDelete(ApiRoutes.DeleteRevisionQuestionsForUser)]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<ApiResponse> DeleteRevisionQuestionForUser()
        {
            // Get the user mail
            var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);

            // Get the user
            var user = await userManager.FindByEmailAsync(email);

            // If the user is null
            if(user is null)
            {
                // Return error message
                return new ApiResponse
                {
                    ErrorMessage = "User not found"
                };
            }

            // Create a new specification
            var spec = new RevisionQuestionsSpecification(user.Id);

            // Fetch the revision questions associated with this user
            var revisionQuestions = await unit.Repository<RevisionQuestionsDataModel>().GetAllDataAsync(spec);

            // We delete the questions
            unit.Repository<RevisionQuestionsDataModel>().DeleteDataRange(revisionQuestions);

            // Save the changes
            await unit.Complete();

            // Return the api response
            return new ApiResponse
            {
                Result = "Questions deleted"
            };

        }

        /// <summary>
        /// To delete a fesor question
        /// </summary>
        /// <returns></returns>
        [HttpDelete(ApiRoutes.DeleteQuestionById)]
		[Authorize(Policy = "RequireAdminClaim")]
		public async Task<ApiResponse> DeleteFesorQuestionById([FromQuery] int questionId)
        {
            // Initialize a spec
            var spec = new FesorQuestionsSpecification(questionId);

            // Fetch the question
            var question = await unit.Repository<FesorQuestionsDataModel>().GetDataWithSpec(spec);

            // Delete the question
            unit.Repository<FesorQuestionsDataModel>().DeleteData(question);

            // Save the changes
            await unit.Complete();

            return new ApiResponse
            {
                Result = "Question deleted"
            };
        }


        //public async Task<ApiResponse> UpdateFesorQuestions()
        //{

        //}

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
