using BibleQuiz.Domain.Models;
using BibleQuiz.Domain.Shared;

namespace BibleQuiz.Domain.Services;
public interface IHttpClient
{
    Task<Result<TResult, TError>> SendAsync<TBody, TResult, TError>
            (HttpRequest<TBody> requestBody) where TError: Error;
}
