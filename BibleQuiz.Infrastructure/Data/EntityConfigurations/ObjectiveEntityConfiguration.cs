using BibleQuiz.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleQuiz.Infrastructure.Data.EntityConfigurations;
internal sealed class ObjectiveEntityConfiguration : IEntityTypeConfiguration<Objective>
{
    public void Configure(EntityTypeBuilder<Objective> builder)
    {
        builder.ToTable(nameof(Objective), QuizDbContext.COMMON);

        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.Options)
            .WithOwner();


    }
}
