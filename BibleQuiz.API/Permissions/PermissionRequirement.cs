using Microsoft.AspNetCore.Authorization;

namespace BibleQuiz.API.Permissions;

public class PermissionRequirement : IAuthorizationRequirement
{
    public PermissionRequirement(string permission)
    {
        Permission = permission;
    }
    public string Permission { get; set; }
}
