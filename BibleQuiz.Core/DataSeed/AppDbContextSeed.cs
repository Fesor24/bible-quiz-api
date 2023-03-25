using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BibleQuiz.Core
{
    public class AppDbContextSeed
    {      
        /// <summary>
        /// Method to seed data 
        /// </summary>
        public static async Task SeedDataAsync(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ThousandQuizQuestions.Any())
                {
                    // Read the content of the file
                    var thousandQuestions = File.ReadAllText("../BibleQuiz.Core/DataSeed/ThousandQuestions.json");

                    // Deserialize it into a list of thousand questions
                    var questions = JsonSerializer.Deserialize<List<ThousandQuizQuestionsDataModel>>(thousandQuestions);

                    // We loop over the questions and add it to db
                    foreach (var question in questions)
                    {
                        await context.ThousandQuizQuestions.AddAsync(question);
                    }

                    // Save the changes to db
                    await context.SaveChangesAsync();

                }

                if(!context.FesorQuestions.Any())
                {
                    // Read the content from the file
                    var fesorsQuestion = File.ReadAllText("../BibleQuiz.Core/DataSeed/fesor.json");

                    // Deserialize it into a list of fesor questions
                    var questions = JsonSerializer.Deserialize<List<FesorQuestionsDataModel>>(fesorsQuestion);

                    // Loop over the questions and add to db
                    foreach(var question in questions)
                    {
                        // Add the question to db
                        await context.FesorQuestions.AddAsync(question);
                    }

                    // Save the changes to db
                    await context.SaveChangesAsync();
                }

            }

            catch(Exception ex)
            {
                // Create logger
                var logger = loggerFactory.CreateLogger<AppDbContextSeed>();

                // Log error to console
                logger.LogError("An error occurred", ex.Message);
            }
        }

        /// <summary>
        /// To seed user to db
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="loggerFactory"></param>
        /// <returns></returns>
        public static async Task SeedUserAsync(UserManager<ApplicationUser> userManager,ILoggerFactory loggerFactory)
        {
            try
            {
                // If we have no user in db
                if (!userManager.Users.Any())
                {
                    // Create the user
                    var result = await userManager.CreateAsync(new ApplicationUser
                    {
                        Email = "fesor@gtcc.admin.com",
                        UserName = "fesor@gtcc.admin.com",
                        FirstName = "Fesor",
                        LastName = "Dev",
                        Permission = Permission.Granted
                    }, "Passw0rd_123");

                    if (result.Succeeded)
                    {
                        // Find the user by mail
                        var user = await userManager.FindByEmailAsync("fesor@gtcc.admin.com");

                        // Add claimes to the user
                        await userManager.AddClaimsAsync(user, new List<Claim>
                        {
                            new Claim(ClaimTypes.Role, "Admin"),
                            new Claim("premiumuser", "PremiumUser")
                        });

                        // Giving the user admin role
                        //await userManager.AddToRoleAsync(user, "Admin");
                    }
                }
            }
            catch(Exception ex)
            {
                // Create a logger of tyoe AppDbContext
                var logger = loggerFactory.CreateLogger(typeof(AppDbContextSeed));

                // Log error to console
                logger.LogError("An error ocurred. Details: {error}", ex.Message);
            }
        }

        /// <summary>
        /// Seed roles in the db
        /// </summary>
        /// <param name="roleManager"></param>
        /// <param name="loggerFactory"></param>
        /// <returns></returns>
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager, ILoggerFactory loggerFactory)
        {
            try
            {
                // Check if the admin role exist
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    // Create an instance of identity role
                    var newRole = new IdentityRole("Admin");

                    // Add the role to the table
                    await roleManager.CreateAsync(newRole);
                }

                // Check if premium user role exists
                if(!await roleManager.RoleExistsAsync("PremiumUser"))
                {
                    // Create it if it doesn't
                    await roleManager.CreateAsync(new IdentityRole
                    {
                        Name = "PremiumUser"
                    });
                }

                // Check if user role exist
                if (!await roleManager.RoleExistsAsync("User"))
                {
                    // Create it if it does not
                    await roleManager.CreateAsync(new IdentityRole
                    {
                        Name = "User"
                    });
                }
            }
            catch(Exception ex)
            {
                // Create a logger of type app db context seed
                var logger = loggerFactory.CreateLogger(typeof(AppDbContextSeed));

                // Log the error to console
                logger.LogError("An erro occurred. Details: {error}", ex.Message);
            }
        }
    }
}
