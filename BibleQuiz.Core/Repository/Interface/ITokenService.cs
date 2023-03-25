using System.Security.Claims;

namespace BibleQuiz.Core
{
	public interface ITokenService
	{
		string CreateToken(ApplicationUser user, IEnumerable<Claim> userClaims);
	}
}
