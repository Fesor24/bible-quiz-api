using System.Net;
using BibleQuiz.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BibleQuiz.API.Controllers
{
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> userManager;

		private readonly SignInManager<ApplicationUser> signInManager;

		private readonly ITokenService tokenService;

		private readonly IUnitOfWork unitOfWork;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
			ITokenService tokenService, IUnitOfWork unitOfWork)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.tokenService = tokenService;
			this.unitOfWork = unitOfWork;
		}

		/// <summary>
		/// Endpoint to register a user
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost(ApiRoutes.Register)]
		public async Task<ApiResponse> CreateUserAsync([FromBody] RegisterCredentialsApiModel model)
		{
			// Check if we have existing user in db
			var userExist = await userManager.FindByEmailAsync(model.Email);

			// Check if the user is in db
			if (userExist is not null)
			{
				return new ApiResponse
				{
					ErrorMessage = "Email in use"
				};
			}

			// Create the user
			var result = await userManager.CreateAsync(new ApplicationUser
			{
				Email = model.Email,
				FirstName = model.FirstName,
				LastName = model.LastName,
				UserName = model.Email,
				Permission = Permission.Denied
			}, model.Password);

			// If it fails
			if (!result.Succeeded)
			{
				return new ApiResponse
				{
					ErrorMessage = "Failed to create user",
					ErrorResult = result.Errors
				};
			}

			// Fetch the user
			var user = await userManager.FindByEmailAsync(model.Email);

			// Sign in the user
			await signInManager.SignInAsync(user, true);

			// Fetch the claims
			var claims = await userManager.GetClaimsAsync(user);

			// Create instance of userApiModel
			var userApiModel = new UserApiModel();

			// Assign the email to it
			userApiModel.Email = user.Email;

			userApiModel.Token = tokenService.CreateToken(user, claims);

			userApiModel.Permission = user.Permission;

			return new ApiResponse
			{
				Result = userApiModel
			};
		}

		/// <summary>
		/// Endpoint to login a user
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost(ApiRoutes.Login)]
		public async Task<ApiResponse> LoginAsync([FromBody] LoginCredentialsApiModel model)
		{
			// Fetch the user
			var user = await userManager.FindByEmailAsync(model.Email);

			// If user is null
			if (user is null)
			{
				return new ApiResponse
				{
					ErrorMessage = "Unauthorized"
				};
			}

			// Sign in the user
			var result = await signInManager.PasswordSignInAsync(user, model.Password, isPersistent:true, false);

			if (!result.Succeeded)
			{
				return new ApiResponse
				{
					ErrorMessage = "Unauthorized"
				};
			}

			// Create instance of userApiModel
			var userApiModel = new UserApiModel();

			var claims = await userManager.GetClaimsAsync(user);

			// Assign the email
			userApiModel.Email = user.Email;

			userApiModel.Token = tokenService.CreateToken(user, claims);

			userApiModel.Permission = user.Permission;

			return new ApiResponse
			{
				Result = userApiModel
			};
		}

		/// <summary>
		/// Endpoint to grant access to third parties to the question
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost(ApiRoutes.GrantAccess)]
		[Authorize(Policy = "RequireAdminClaim")]
		public async Task<ApiResponse> GrantAccessToResource([FromBody] GrantAccessApiModel model)
		{
			// Get the user
			var user = await userManager.FindByEmailAsync(model.Email);

			// If the user is null
			if (user is null)
			{
				Response.StatusCode = (int)HttpStatusCode.NotFound;

				return new ApiResponse
				{
					ErrorMessage = "User not found"
				};
			}

			// Give the user role
			var result = await userManager.AddClaimAsync(user, new Claim("premiumuser", "PremiumUser"));

			// If it fails
			if (!result.Succeeded)
			{
				return new ApiResponse
				{
					ErrorMessage = "Failed to add claim to user",
					ErrorResult = result.Errors
				};
			}

			// Update the user permission
			user.Permission = Permission.Granted;

			// Update the user table
			await userManager.UpdateAsync(user);

			// Persist the changes
			await unitOfWork.Complete();

			// Return message to client
			return new ApiResponse
			{
				Result = new { Message = "Claim added to user" }
			};
		}
	}
}
