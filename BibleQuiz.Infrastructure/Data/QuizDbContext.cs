﻿using BibleQuiz.Domain.Entities;
using BibleQuiz.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BibleQuiz.Infrastructure.Data;
internal class QuizDbContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string>, IdentityUserRole<string>,
    IdentityUserLogin<string>, RoleClaim, IdentityUserToken<string>>
{
    internal const string SECURITY = "Security";
    internal const string COMMON = "com";

    public QuizDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Theory> TestQuestions => Set<Theory>();
    public DbSet<Verse> Verse => Set<Verse>();

    public DbSet<BibleBook> BibleBook => Set<BibleBook>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach(var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        {
            property.SetColumnType("decimal(18,2)");
        }

        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
