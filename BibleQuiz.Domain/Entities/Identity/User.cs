using Microsoft.AspNetCore.Identity;

namespace BibleQuiz.Domain.Entities.Identity;
public class User : IdentityUser
{
    public bool IsActive { get; set; }
}
