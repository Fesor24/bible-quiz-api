using System.Collections.ObjectModel;

namespace BibleQuiz.Shared.Authorization;
public record AppPermission(string Feature, string Action, string Group, string Description, bool IsUser = false)
{
    public string Name => NameFor(Feature, Action);
    public static string NameFor(string feature, string action) =>
        $"Permission.{feature}.{action}";
}

public class AppPermissions
{
    private static readonly AppPermission[] _all = new AppPermission[]
    {
        new(AppFeature.Questions, AppAction.Create, AppRoleGroup.ManagementHierarchy, "Create question"),
        new(AppFeature.Questions, AppAction.Delete, AppRoleGroup.ManagementHierarchy, "Delete question"),
        new(AppFeature.Questions, AppAction.Update, AppRoleGroup.ManagementHierarchy, "Update question"),
        new(AppFeature.Questions, AppAction.Read, AppRoleGroup.ManagementHierarchy, "Read questions", true),

        new(AppFeature.Users, AppAction.Create, AppRoleGroup.SystemAccess, "Create user"),
        new(AppFeature.Users, AppAction.Update, AppRoleGroup.SystemAccess, "Update user"),
        new(AppFeature.Users, AppAction.Delete, AppRoleGroup.SystemAccess, "Delete user"),
        new(AppFeature.Users, AppAction.Read, AppRoleGroup.SystemAccess, "Read user"),

        new(AppFeature.UserRoles, AppAction.Update, AppRoleGroup.SystemAccess, "Update user role"),
        new(AppFeature.UserRoles, AppAction.Read, AppRoleGroup.SystemAccess, "Read user role"),

        new(AppFeature.RoleClaims, AppAction.Read, AppRoleGroup.SystemAccess, "Read role claims/permissions"),
        new(AppFeature.RoleClaims, AppAction.Update, AppRoleGroup.SystemAccess, "Update role claims/permissions"),
    };

    public static IReadOnlyList<AppPermission> Admin = new ReadOnlyCollection<AppPermission>(
        _all.Where(x => !x.IsUser).ToList());

    public static IReadOnlyList<AppPermission> User = new ReadOnlyCollection<AppPermission>(
        _all.Where(x => x.IsUser).ToList());
}
