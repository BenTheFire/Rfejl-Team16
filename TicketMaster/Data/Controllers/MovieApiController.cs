using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Ticketmaster.Data.DTOs;
using Ticketmaster.Data.Services.Interfaces;
using Ticketmaster.Objects;

namespace Ticketmaster.Data.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MovieApiController : ControllerBase
    {
        private IMovieService movieService;

        public MovieApiController(IMovieService iMovieService)
        {
            movieService = iMovieService;
        }

        [HttpGet("{imdbId}")]
        public async Task<IActionResult> GetMovieAsync(string imdbId)
        {
            try
            {
                MovieWithCast result = await movieService.FetchMovieDataAsync(imdbId);
                return Ok(result);
            }
            catch (MovieDbException)
            {
                return NotFound();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{imdbId}/screenings")]
        public async Task<IActionResult> GetScreeningsAsync(string imdbId)
        {
            try
            {
                var result = await movieService.FetchScreenings(imdbId);
                return Ok(result);
            }
            catch (MovieDbException)
            {
                return NotFound();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
