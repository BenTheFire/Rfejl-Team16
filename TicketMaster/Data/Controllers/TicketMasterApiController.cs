using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketMaster.Data.Services.Interfaces;
using TicketMaster.Objects;

namespace TicketMaster.Data.Controllers
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
        public async Task<IActionResult> GetAsync(int start, int end)
        {
            try
            {
                List<Movie> result = await tms.FetchMoviesBetweenAsync(start, end);

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
