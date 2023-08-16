using System.Collections.Generic;
using MovieFetcher.Models;

namespace MovieFetcherTest.TestsData;

public class ConvertedResponseMockData
{
    public static readonly ResponseData ResponseData = new()
    {
        moviesByTitle = new List<Movie>
        {
            new Movie { Title = "The Dark Redemption", Year = 1994 },
            new Movie { Title = "The Dark", Year = 1972 },
            new Movie { Title = "The Dark Knight", Year = 2008 },
            new Movie { Title = "The Dark Knight", Year = 2008 }
        },
        amountOfMoviesByYear = new Dictionary<int, int>
        {
            { 1994, 1 },
            { 1972, 1 },
            { 2008, 2 }
        }
    };
}