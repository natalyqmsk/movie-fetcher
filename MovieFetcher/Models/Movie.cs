namespace MovieFetcher.Models;

public class Movie
{
    public string Title { get; set; }
    public int Year { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is Movie movie && Title == movie.Title && Year == movie.Year;
    }
}