using AutoMapper;
using MovieFetcher.Models;

namespace MovieFetcher.Mapper;

public class ResponseProfile : Profile
{
    public ResponseProfile()
    {
        CreateMap<MovieFromResponse, Movie>();
        CreateMap<IEnumerable<Movie>, ResponseData>()
            .ForMember(
                dest => dest.moviesByTitle,
                opt =>
                    opt.MapFrom(src => src.Where(ValidateTitleAndYear)))
            .ForMember(
                dest => dest.amountOfMoviesByYear,
                opt =>
                    opt.MapFrom(src => src.GroupBy(x => x.Year)
                        .Select(x => new { x.Key, Count = x.Count() })
                        .Where(x => x.Key > 1900)
                        .ToDictionary(x => x.Key, x => x.Count)));
    }

    private static bool ValidateTitleAndYear(Movie x) => x.Title != null && x.Year > 1900;
}