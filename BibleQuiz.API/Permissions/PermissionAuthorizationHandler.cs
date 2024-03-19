using BibleQuiz.Shared.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace BibleQuiz.API.Permissions;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
        PermissionRequirement requirement)
    {
        if (context.User is null)
            return Task.CompletedTask;

        var permissions = context.User.Claims
            .Where(x => x.Type == AppClaim.Permission && 
            x.Issuer == "LOCAL AUTHORITY" &&
            x.Value == requirement.Permission)
            .ToList();

        if (permissions.Any())
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
