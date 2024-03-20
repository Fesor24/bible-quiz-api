using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleQuiz.Infrastructure.Data.EntityConfigurations.Identity;
internal sealed class IdentityUserRoleEntityConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.ToTable("IdentityUserRole", QuizDbContext.SECURITY);
    }
}
