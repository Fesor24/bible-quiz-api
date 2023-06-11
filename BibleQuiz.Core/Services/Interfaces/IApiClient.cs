namespace BibleQuiz.Core
{
    public interface IApiClient
    {
        Task<ApiResponse<TResult, TWarningResult, TErrorResult>> SendAsync<TBody, TResult, TWarningResult, TErrorResult>
            (BibleApiRequest<TBody> requestBody);
    }
}
