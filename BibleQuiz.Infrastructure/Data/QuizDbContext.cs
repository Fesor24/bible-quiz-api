using BibleQuiz.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BibleQuiz.Infrastructure.Data;
public class QuizDbContext : IdentityDbContext<User>
{
    internal const string USER = "usr";
    internal const string COMMON = "com";

    public DbSet<TestQuestion> TestQuestions => Set<TestQuestion>();
    public DbSet<Verse> Verse => Set<Verse>();
}
