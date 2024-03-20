using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleQuiz.Infrastructure.Data.EntityConfigurations.Identity;
internal sealed class IdentityUserClaimEntityConfiguration : IEntityTypeConfiguration<IdentityUserClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder)
    {
        builder.ToTable("IdentityUserClaim", QuizDbContext.SECURITY);
    }
}
