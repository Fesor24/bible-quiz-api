using BibleQuiz.Domain.Models;
using BibleQuiz.Domain.Services;
using BibleQuiz.Domain.Shared;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BibleQuiz.Application.Services;
public class CustomHttpClient : IHttpClient
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CustomHttpClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<Result<TResult, TError>> SendAsync<TBody, TResult, TError>
        (HttpRequest<TBody> requestBody) where TError : Error
    {
        var response = default(HttpResponseMessage);

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        using (var client = _httpClientFactory.CreateClient())
        {
            foreach(var header in requestBody.Headers)
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }

            response = await client.SendAsync(new HttpRequestMessage
            {
                Method = requestBody.Method,
                RequestUri = new Uri(requestBody.Uri),
                Content = JsonContent.Create(requestBody.Content),
            });
        }

        if (response.IsSuccessStatusCode)
        {
            var serializedResult = await response.Content.ReadFromJsonAsync<TResult>(jsonOptions);

            return new Result<TResult, TError>(value: serializedResult);
        }
        else
        {
            var serializedErrorResult = await response.Content.ReadFromJsonAsync<object>(jsonOptions);

            Error error = new("Bible Quiz External Api Error", JsonSerializer.Serialize(serializedErrorResult));

            return new Result<TResult, TError>(error: (TError)error, false);
        }
    }
}
