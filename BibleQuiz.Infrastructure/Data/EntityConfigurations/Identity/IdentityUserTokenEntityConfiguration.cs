using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleQuiz.Infrastructure.Data.EntityConfigurations.Identity;
internal sealed class IdentityUserTokenEntityConfiguration : IEntityTypeConfiguration<IdentityUserToken<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder)
    {
        builder.ToTable("IdentityUserToken", QuizDbContext.SECURITY);
    }
}
