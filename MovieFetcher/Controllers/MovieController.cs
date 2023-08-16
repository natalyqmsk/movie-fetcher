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

        [HttpGet("/title", Name = "GetMovieByTitle")]
        public async Task<IActionResult> GetMovieByTitle([FromQuery] RequestParameters request)
        {
            var moviesByTitle = await _movieService.GetMoviesByTitle(request);
            return !moviesByTitle.moviesByTitle.Any() ? NotFound() : Ok(moviesByTitle);
        }
    }
}