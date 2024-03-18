using Microsoft.AspNetCore.Identity;

namespace BibleQuiz.Domain.Entities.Identity;
public class RoleClaim : IdentityRoleClaim<string>
{
    public string Description { get; set; }
    public string Group { get; set; }
}
