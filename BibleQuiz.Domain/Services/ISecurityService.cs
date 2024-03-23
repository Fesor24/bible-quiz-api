using BibleQuiz.Domain.Entities.Identity;
using BibleQuiz.Domain.Shared;

namespace BibleQuiz.Domain.Services;
public interface ISecurityService
{
    Task<Result<User, Error>> VerifyUserCredentials(string userName, string password);

    Task<Result<string, Error>> CreateTokenAsync(string userName, string password);

    Task<Result<string, Error>> CreateUserAsync(string userName, string password);
}
