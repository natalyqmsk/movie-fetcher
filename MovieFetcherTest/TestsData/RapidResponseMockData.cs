using System.Collections.Generic;
using System.Text.Json.Nodes;
using MovieFetcher.Models;

namespace MovieFetcherTest.TestsData;

public class RapidResponseMockData
{
    public static readonly RapidResponse RapidResponse = new()
    {
        results = new List<MovieFromResponse>
        {
            MovieFromResponseMockData.MovieFromResponse1,
            MovieFromResponseMockData.MovieFromResponse2,
            MovieFromResponseMockData.MovieFromResponse3,
            MovieFromResponseMockData.MovieFromResponse4
        }
    };
}

public class MovieFromResponseMockData
{
    public static readonly MovieFromResponse MovieFromResponse1 = new()
    {
        title = "The Dark Redemption",
        year = 1994,
    };

    public static readonly MovieFromResponse MovieFromResponse2 = new()
    {
        title = "The Dark",
        year = 1972,
    };

    public static readonly MovieFromResponse MovieFromResponse3 = new()
    {
        title = "The Dark Knight",
        year = 2008,
    };

    public static readonly MovieFromResponse MovieFromResponse4 = new()
    {
        title = "The Dark Knight",
        year = 2008,
    };
}