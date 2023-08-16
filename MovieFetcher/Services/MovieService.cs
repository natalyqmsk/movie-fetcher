using AutoMapper;
using MovieFetcher.Models;

namespace MovieFetcher.Services;

public class MovieService : IMovieService
{
    private readonly IHttpService _httpService;
    private readonly IMapper _mapper;

    public MovieService(IHttpService httpService, IMapper mapper)
    {
        _httpService = httpService;
        _mapper = mapper;
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

    private ResponseData BuildResponse(IEnumerable<MovieFromResponse> movies)
    {
        var moviesByTitle = _mapper.Map<IEnumerable<Movie>>(movies);
        return _mapper.Map<ResponseData>(moviesByTitle);
    }
}