using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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

			claims = new List<Claim>
			{
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.GivenName, user.FirstName),
				new Claim("familyname", user.LastName),

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
