using Microsoft.AspNetCore.Mvc;
using MovieFetcher.Models;
using MovieFetcher.Services;

namespace MovieFetcher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("{title}", Name = "GetMovie")]
        public async Task<ResponseData> GetMovie(string title)
        {
            return await _movieService.GetMoviesByTitle(title);
        }
    }
}