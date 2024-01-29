using BibleQuiz.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleQuiz.Infrastructure.Data.EntityConfigurations;
internal sealed class BibleBookEntityConfiguration : IEntityTypeConfiguration<BibleBook>
{
    public void Configure(EntityTypeBuilder<BibleBook> builder)
    {
        builder.ToTable(nameof(BibleBook), QuizDbContext.COMMON);
        builder.HasKey(x => x.Id);
    }
}
