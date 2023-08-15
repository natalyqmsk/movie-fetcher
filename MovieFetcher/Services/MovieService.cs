using MovieFetcher.Models;

namespace MovieFetcher.Services;

public class MovieService : IMovieService
{
    private readonly IHttpService _httpService;

    public MovieService(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<ResponseData> GetMoviesByTitle(string title)
    {
        var response = await _httpService.CreateGetAsync<RapidResponse>(BuildUri(title));
        return BuildResponse(response.results);
    }

    private static UriBuilder BuildUri(string title)
    {
        var limit = "100";
        return new()
        {
            Port = -1,
            Path = "title/v2/find",
            Host = "imdb8.p.rapidapi.com",
            Scheme = "https",
            Query = $"title={title}&limit={limit}&paginationKey=0&sortArg=moviemeter%2Casc"
        };
    }

    private static Movie BuildMovie(MovieFromResponse x) => new() { Title = x.title, Year = x.year };

    private static Dictionary<int, int> GroupByYear(IEnumerable<Movie> movies) =>
        movies.GroupBy(x => x.Year).ToDictionary(x => x.Key, x => x.Count());

    private static ResponseData BuildResponse(IEnumerable<MovieFromResponse> movies)
    {
        var moviesByTitle = movies.Select(BuildMovie);
        var byTitle = moviesByTitle.ToList();
        return new()
            { moviesByTitle = byTitle, amountOfMoviesByYear = GroupByYear(byTitle) };
    }
}