using System.Text.Json;
using System.Web;
using Polly;
using Polly.Retry;

namespace MovieFetcher.Services;

public class HttpService : IHttpService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<HttpService> _logger;

    public HttpService(IHttpClientFactory httpClientFactory, ILogger<HttpService> logger)
    {
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient("RapidAPI");
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(
            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", "efa5177e3emshe39ef2a0d31f3dbp1500f3jsnf42ace61607f");
    }

    public async Task<T> CreateGetAsync<T>(UriBuilder uriBuilder)
    {
        _logger.LogInformation($"CreateGetAsync : {uriBuilder}");
        var retryPolicy = GetRetryPolicy();
        var response =
            await retryPolicy.ExecuteAsync(() => _httpClient.GetAsync(uriBuilder.Uri));
        var data = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(response.StatusCode + response.ReasonPhrase);
        }

        return JsonSerializer.Deserialize<T>(data)!;
    }

    private AsyncRetryPolicy GetRetryPolicy()
    {
        return Policy.Handle<HttpRequestException>()
            .WaitAndRetryAsync(retryCount: 3, _ => TimeSpan.FromSeconds(1),
                (exception, timeSpan, retryCount, context) =>
                {
                    _logger.LogError(exception, $"Retry attempt {retryCount} failed");
                });
    }
}