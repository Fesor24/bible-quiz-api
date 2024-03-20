namespace BibleQuiz.Domain.Services;
public interface ITokenService
{
    Task<string> CreateTokenAsync();
}
