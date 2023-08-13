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

		private readonly ApplicationDbContext context;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
			ITokenService tokenService, IUnitOfWork unitOfWork, ApplicationDbContext context)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.tokenService = tokenService;
			this.unitOfWork = unitOfWork;
			this.context = context;
		}

		/// <summary>
		/// Endpoint to register a user
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost(ApiRoutes.Register)]
        [Authorize(Policy = "RequireAdminClaim")]
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
				UserName = model.Email,
				// Change to denied if we want to use admin to grant access
				Permission = Permission.Granted
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

			// Add claims to user
			//Remove if we want to grant access
			await userManager.AddClaimAsync(user, new Claim("premiumuser", "PremiumUser"));

			// Sign in the user
			await signInManager.SignInAsync(user, true);

			// Fetch the claims
			var claims = await userManager.GetClaimsAsync(user);

			// Create instance of userApiModel
			var userApiModel = new UserApiModel();

			// Assign the email to it
			userApiModel.Email = user.Email;
			
			// Pass the token
			userApiModel.Token = tokenService.CreateToken(user, claims);

			// Pass the permission
			userApiModel.Permission = user.Permission;

			// Return api response
			return new ApiResponse
			{
				Result = userApiModel
			};
		}

		/// <summary>
		/// Endpoint to get current user
		/// </summary>
		/// <returns></returns>
		[HttpGet(ApiRoutes.GetCurrentUser)]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<ApiResponse> GetCurrentUser()
		{
			// Get the email of the user
			var email = User.FindFirstValue(ClaimTypes.Email);

			// Get the user with associated emil from db
			var user = await userManager.FindByEmailAsync(email);

			// Get the associated claims of this user
			var userClaims = await userManager.GetClaimsAsync(user);

			// Create instance of userApiModer
			var userApiModel = new UserApiModel
			{
				Email = user.Email,
				Permission = user.Permission,
				Token = tokenService.CreateToken(user, userClaims)
			};

			// Return the result
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
				// Return api response
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
					ErrorMessage = "Invalid username or password"
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
		[HttpGet(ApiRoutes.GrantAccess)]
		[Authorize(Policy = "RequireAdminClaim")]
		public async Task<ApiResponse> GrantAccessToResource([FromQuery] string email)
		{
			// Get the user
			var user = await userManager.FindByEmailAsync(email);

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

		/// <summary>
		/// Endpoint to fetch all users
		/// </summary>
		/// <returns></returns>
		[HttpGet(ApiRoutes.FetchAllUsers)]
		//[Authorize(Policy = "RequireAdminClaim")]
		public ApiResponse FetchAllUsers([FromQuery]int pageSize = 6, [FromQuery]int pageIndex = 1)
		{
			// Fetch all users
			var users = userManager
				.Users
				.Skip((pageIndex -1) * pageSize)
				.Take(pageSize)
				.ToList();

			var usersDto = users.Select(x => new FetchAllUsersApiModel
			{
				Email = x.Email,
				Id = x.Id
			}).ToList();

			return new ApiResponse
			{
				Result = usersDto
			};
		}

		/// <summary>
		/// Endpoint to fetch user by emails
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		[HttpGet(ApiRoutes.FetchUser)]
		[Authorize(Policy = "RequireAdminClaim")]
		public ApiResponse FetchUsersByEmail([FromQuery] string email)
		{
			// Get the users with the specified email
			var users = context.Users
				.Where(x => x.Email.ToLower().Contains(email.ToLower())).ToList();

			// Data to be returned
			var userDto = users.Select(x => new FetchAllUsersApiModel
			{
				Email = x.Email,
				Id = x.Id
			});

			// Return data to client
			return new ApiResponse
			{
				Result = userDto
			};

		}
	}
}
