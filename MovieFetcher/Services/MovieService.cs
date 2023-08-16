using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

    public async Task<ResponseData> GetMoviesByTitle(RequestParameters requestParameters)
    {
        var response = await _httpService.CreateGetAsync<RapidResponse>(BuildUri(requestParameters));
        return BuildResponse(response.results);
    }

    private static UriBuilder BuildUri(RequestParameters requestParameters)
    {
        var title = requestParameters.Title;
        var limit = requestParameters.PageSize;
        var paginationKey = requestParameters.Page;
        var sort = requestParameters.Sort;
        return new()
        {
            Port = -1,
            Path = "title/v2/find",
            Host = "imdb8.p.rapidapi.com",
            Scheme = "https",
            Query = $"title={title}&limit={limit}&paginationKey={paginationKey}&sortArg={sort}"
        };
    }

    private ResponseData BuildResponse(IEnumerable<MovieFromResponse> movies)
    {
        var moviesByTitle = _mapper.Map<IEnumerable<Movie>>(movies);
        return _mapper.Map<ResponseData>(moviesByTitle);
    }
}