namespace MovieFetcher.Services;

public interface IHttpService
{
    Task<T> CreateGetAsync<T>(UriBuilder uriBuilder);

}