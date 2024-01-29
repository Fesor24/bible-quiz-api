using BibleQuiz.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BibleQuiz.Infrastructure.Data;
public class QuizDbContext : DbContext
{
    internal const string USER = "usr";
    internal const string COMMON = "com";

    public QuizDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<TestQuestion> TestQuestions => Set<TestQuestion>();
    public DbSet<Verse> Verse => Set<Verse>();

    public DbSet<BibleBook> BibleBook => Set<BibleBook>();
}
