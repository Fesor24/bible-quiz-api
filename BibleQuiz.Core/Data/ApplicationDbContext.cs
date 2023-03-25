using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BibleQuiz.Core
{
    /// <summary>
    /// Db context class
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>(options =>
            {
                // Setting the mail to be unique in db
                options.HasIndex(e => e.Email).IsUnique();
            });

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Thousand quiz questions db set
        /// </summary>
        public DbSet<ThousandQuizQuestionsDataModel> ThousandQuizQuestions { get; set; }

        /// <summary>
        /// Revision quiz questions db set
        /// </summary>
        public DbSet<RevisionQuestionsDataModel> RevisionQuestions { get; set; }

        /// <summary>
        /// Table containing questions set by me
        /// </summary>
        public DbSet<FesorQuestionsDataModel> FesorQuestions { get; set;}
    }
}
