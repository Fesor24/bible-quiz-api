using System.Collections.ObjectModel;

namespace BibleQuiz.Shared.Authorization;
public static class AppRoles
{
    public const string Admin = nameof(Admin);
    public const string User = nameof(User);

    public static IReadOnlyList<string> Default = 
        new ReadOnlyCollection<string>(new List<string> { Admin, User });

    public static bool IsDefault(string roleName) =>
        Default.Any(x => x == roleName);
}
