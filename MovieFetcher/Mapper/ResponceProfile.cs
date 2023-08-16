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
                    opt.MapFrom(src => src))
            .ForMember(
                dest => dest.amountOfMoviesByYear,
                opt =>
                    opt.MapFrom(src => src.GroupBy(x => x.Year)
                        .Select(x => new { x.Key, Count = x.Count() })
                        .ToDictionary(x => x.Key, x => x.Count)));
    }
}