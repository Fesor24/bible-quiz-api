using BibleQuiz.Domain.Entities;
using BibleQuiz.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleQuiz.Infrastructure.Data.EntityConfigurations;
internal sealed class TheoryEntityConfiguration : IEntityTypeConfiguration<Theory>
{
    public void Configure(EntityTypeBuilder<Theory> builder)
    {
        builder.ToTable(nameof(Theory), QuizDbContext.COMMON);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Source)
            .HasConversion(sr => sr.ToString(),
            c => (QuestionSource)Enum.Parse(typeof(QuestionSource), c));

        builder.Property(x => x.Passage)
            .IsRequired(false);
    }
}
