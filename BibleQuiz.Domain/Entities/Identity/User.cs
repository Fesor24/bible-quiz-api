using Microsoft.AspNetCore.Identity;

namespace BibleQuiz.Domain.Entities.Identity;
public class User : IdentityUser
{
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiry { get; set; }
    public bool IsActive { get; set; }
}
