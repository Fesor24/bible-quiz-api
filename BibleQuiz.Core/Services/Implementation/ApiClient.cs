using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;

namespace BibleQuiz.Core
{
    public class ApiClient : IApiClient
    {
        /// <summary>
        /// DI instance of IHttpClientFactory
        /// </summary>
        private readonly IHttpClientFactory _httpClientFactory;


        /// <summary>
        /// DI instance of Bible credentials
        /// </summary>
        private readonly BibleCredentials _bibleCreds;

        public ApiClient(IHttpClientFactory httpClientFactory, IOptions<BibleCredentials> bibleCreds)
        {
            _httpClientFactory= httpClientFactory;
            _bibleCreds = bibleCreds.Value;
        }

        public async Task<ApiResponse<TResult, TWarningResult, TErrorResult>> SendAsync<TBody, TResult, TWarningResult, TErrorResult>
            (BibleApiRequest<TBody> requestBody)
        {
            // Get the bible api key
            var bibleKey = _bibleCreds.ApiKey;

            // Initialize the http response
            var response = default(HttpResponseMessage);

            // Set the json options
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            using(var client = _httpClientFactory.CreateClient())
            {
                // Add the api key to the header
                client.DefaultRequestHeaders.Add("api-key", bibleKey);

                // Send the request and get rthe response
                response = await client.SendAsync(new HttpRequestMessage
                {
                    Method = requestBody.Method,
                    RequestUri = new Uri(requestBody.Uri),
                    Content = JsonContent.Create(requestBody.Content)
                });
            }

            // If the response was successful
            if (response.IsSuccessStatusCode)
            {
                // Serialize the result to the TResult class
                var serializedResult = await response.Content.ReadFromJsonAsync<TResult>(jsonOptions);

                // Return the response
                return new ApiResponse<TResult, TWarningResult, TErrorResult> { Result = serializedResult };
            }
            else
            {
                // Serialize the error result
                var serializedErrorResult = await response.Content.ReadFromJsonAsync<TErrorResult>(jsonOptions);

                // Return the api response
                return new ApiResponse<TResult, TWarningResult, TErrorResult>
                {
                    ErrorMessage = response.ReasonPhrase,
                    ErrorResult = serializedErrorResult
                };
            }
        }
    }
}
