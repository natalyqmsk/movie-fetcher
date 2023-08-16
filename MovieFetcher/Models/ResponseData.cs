namespace MovieFetcher.Models;

public class ResponseData
{
    public IEnumerable<Movie> moviesByTitle { get; set; }
    public Dictionary<int, int> amountOfMoviesByYear { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is ResponseData responseData
               && moviesByTitle.SequenceEqual(responseData.moviesByTitle)
               && amountOfMoviesByYear.SequenceEqual(responseData.amountOfMoviesByYear);
    }
}