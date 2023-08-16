using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using MovieFetcher.Mapper;
using MovieFetcher.Models;
using MovieFetcher.Services;
using MovieFetcherTest.TestsData;
using Xunit;
using Xunit.Sdk;

namespace MovieFetcherTest;

public class MovieServiceTest
{
    private readonly Mock<IHttpService> _httpService;
    private readonly IMovieService _movieService;

    public MovieServiceTest()
    {
        var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new ResponseProfile()); });
        var mapper = mockMapper.CreateMapper();
        _httpService = new Mock<IHttpService>();
        _movieService = new MovieService(_httpService.Object, mapper);
    }

    [Fact]
    public async Task DataReturnedFromRapidConvertedToResponse()
    {
        _httpService.Setup(x =>
                x.CreateGetAsync<RapidResponse>(It.IsAny<UriBuilder>()))
            .ReturnsAsync(RapidResponseMockData.RapidResponse);
        var actual = await _movieService.GetMoviesByTitle("star wars");
        _httpService.Verify(x => x.CreateGetAsync<RapidResponse>(It.IsAny<UriBuilder>())
            , Times.Once);

        Assert.Equal(ConvertedResponseMockData.ResponseData, actual);
    }

    [Fact]
    public async Task WhenNoDataReturnedFromRapidEmptyObjectReturned()
    {
        var empty = new RapidResponse();
        _httpService.Setup(x =>
                x.CreateGetAsync<RapidResponse>(It.IsAny<UriBuilder>()))
            .ReturnsAsync(empty);
        var actual = await _movieService.GetMoviesByTitle("star wars");
        _httpService.Verify(x => x.CreateGetAsync<RapidResponse>(It.IsAny<UriBuilder>())
            , Times.Once);

        Assert.Empty(actual.moviesByTitle);
        Assert.Empty(actual.amountOfMoviesByYear);
    }
}