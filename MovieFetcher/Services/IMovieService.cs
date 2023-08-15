using MovieFetcher.Models;

namespace MovieFetcher.Services;

public interface IMovieService
{
    Task<ResponseData> GetMoviesByTitle(string title);
}