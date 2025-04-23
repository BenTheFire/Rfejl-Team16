using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketMaster.Objects;

namespace TicketMaster.Data
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

        [HttpGet]
        public async Task<IActionResult> GetAsync(string imdbId)
        {
            try
            {
                Movie result = await movieService.FetchMovieDataAsync(imdbId);

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
