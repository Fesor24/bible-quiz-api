using BibleQuiz.Domain.Entities;
using BibleQuiz.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleQuiz.Infrastructure.Data.EntityConfigurations;
internal sealed class TestQuestionEntityConfiguration : IEntityTypeConfiguration<TestQuestion>
{
    public void Configure(EntityTypeBuilder<TestQuestion> builder)
    {
        builder.ToTable(nameof(TestQuestion), QuizDbContext.COMMON);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Source)
            .HasConversion(sr => sr.ToString(),
            c => (QuestionSource)Enum.Parse(typeof(QuestionSource), c));
    }
}
