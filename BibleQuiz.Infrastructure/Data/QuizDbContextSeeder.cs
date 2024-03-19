using BibleQuiz.Domain.Entities.Identity;
using BibleQuiz.Shared.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BibleQuiz.Infrastructure.Data;
public class QuizDbContextSeeder
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly QuizDbContext _context;

    public QuizDbContextSeeder(UserManager<User> userManager, RoleManager<Role> roleManager,
        QuizDbContext context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
    }

    public async Task SeedDataAsync()
    {
        await CheckAndApplyMigration();
        await SeedRoles();
        await SeedUser();
    }

    private async Task CheckAndApplyMigration()
    {
        if(_context.Database.GetPendingMigrations().Any())
            await _context.Database.MigrateAsync();
    }

    private async Task SeedRoles()
    {
        foreach(var roleName in AppRoles.Default)
        {
            if (await _roleManager.Roles.AnyAsync(x => x.Name == roleName))
                continue;

            Role role = new()
            {
                Id = Guid.NewGuid().ToString(),
                Name = roleName,
                Description = $"{roleName} role",
            };

            await _roleManager.CreateAsync(role);

            if (roleName == AppRoles.Admin)
                await AssignPermissionsToRole(role, AppPermissions.Admin);

            if (roleName == AppRoles.User)
                await AssignPermissionsToRole(role, AppPermissions.User);
        }
    }

    private async Task SeedUser()
    {
        if (_userManager.Users.Any())
            return;

        string userName = "Fesor";
        string email = "fesor@gtcc.admin.com";

        User user = new()
        {
            Id= Guid.NewGuid().ToString(),
            Email = email,
            UserName = userName,
            IsActive = true,
            EmailConfirmed = true,
            NormalizedEmail = email.ToUpperInvariant(),
            NormalizedUserName = userName.ToUpperInvariant()
        };

        var res = await _userManager.CreateAsync(user, "Passw0rd_123");

        if (res.Succeeded)
        {
            if(!await _userManager.IsInRoleAsync(user, AppRoles.User) && 
                !await _userManager.IsInRoleAsync(user, AppRoles.Admin))
            {
                await _userManager.AddToRolesAsync(user, AppRoles.Default);
            }
        }
    }

    private async Task AssignPermissionsToRole(Role role, IReadOnlyList<AppPermission> permissions)
    {
        var currentClaims = await _roleManager.GetClaimsAsync(role);

        List<RoleClaim> roleClaims = new();

        foreach(var permission in permissions)
        {
            if(!currentClaims.Any(x => x.Value == permission.Name && x.Type == AppClaim.Permission))
            {
                RoleClaim roleClaim = new()
                {
                    ClaimType = AppClaim.Permission,
                    RoleId = role.Id,
                    ClaimValue = permission.Name,
                    Description = permission.Description,
                    Group = permission.Group
                };

                roleClaims.Add(roleClaim);
            }
        }

        if (roleClaims.Any())
        {
            await _context.RoleClaims.AddRangeAsync(roleClaims);

            await _context.SaveChangesAsync();
        }
    }
}
