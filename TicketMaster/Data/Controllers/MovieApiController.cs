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

        public MovieApiController(IMovieService iMovieService, ITicketMasterService itms)
        {
            movieService = iMovieService;
        }

        [HttpGet("{imdbId}")]
        public async Task<IActionResult> GetAsync(string imdbId)
        {
            try
            {
                MovieWithCast result = await movieService.FetchMovieDataAsync(imdbId);

                if (result == null)
                {
                    return BadRequest("Imdb ID is not found in our database");
                }
                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
