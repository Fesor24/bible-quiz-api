using Microsoft.AspNetCore.Identity;

namespace BibleQuiz.Core
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }   

        public string LastName { get; set; }


    }
}
