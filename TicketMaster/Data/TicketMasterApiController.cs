using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketMaster.Objects;

namespace TicketMaster.Data
{
    [Route("api")]
    [ApiController]
    public class TicketMasterApiController : ControllerBase
    {
        private ITicketMasterService tms;

        public TicketMasterApiController(ITicketMasterService itms)
        {
            tms = itms;
        }

        [HttpGet]
        public IActionResult Get(int start, int end)
        {
            try
            {
                List<Movie> result = tms.FetchMoviesBetween(start, end);

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
