using BibleQuiz.Application.Constants;
using BibleQuiz.Domain.Shared;

namespace BibleQuiz.Application.Errors;
internal static class ApplicationError
{
    internal static class TestQuestion
    {
        internal static Error NotFound(int id) => new(ApplicationStatusCodes.NOTFOUND, 
            $"Question with Id:{id} not found");
    }
}
