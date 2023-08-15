namespace MovieFetcher.Models;

public class ResponseData
{
    public IEnumerable<Movie> moviesByTitle { get; set; }
    public Dictionary<int, int> amountOfMoviesByYear { get; set; }
}