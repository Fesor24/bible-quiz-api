using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BibleQuiz.Core
{
	public class TokenService: ITokenService
	{
		private readonly IConfiguration config;

		private readonly SymmetricSecurityKey key;

		public TokenService(IConfiguration config)
		{
			this.config = config;
			key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]));
		}

		public string CreateToken(ApplicationUser user, IEnumerable<Claim> userClaims)
		{
			var claims = new List<Claim>();

			// Extract values before @ in mail
			var emailSplit = user.Email.Split('@');

			// Initialize the username
			string userName = default;

			// If email split has a length of 2
			if(emailSplit is { Length: 2 })
			{
				userName = emailSplit[0];
			}

			claims = new List<Claim>
			{
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.GivenName, userName ??= "user"),

			};

			// Fetch the claims of the user
			foreach (var claim in userClaims)
			{
				claims.Add(new Claim(claim.Type, claim.Value));
			}

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(3),
				SigningCredentials = creds,
				Issuer = config["JwtSettings:Issuer"],

			};

			var tokenHandler = new JwtSecurityTokenHandler();

			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}
	}
}
