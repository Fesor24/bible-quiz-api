using Microsoft.AspNetCore.Identity;

namespace BibleQuiz.Domain.Entities.Identity;
public class Role : IdentityRole<string>
{
    public string Description { get; set; }
}
