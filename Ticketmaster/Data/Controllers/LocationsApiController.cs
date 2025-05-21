using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticketmaster.Data.Services.Interfaces;

namespace Ticketmaster.Data.Controllers
{
    [Route("api/locations")]
    [ApiController]
    public class LocationsApiController : ControllerBase
    {
        public ILocationService ILS;
        public LocationsApiController(ILocationService input_LS)
        {
            ILS = input_LS;
        }
        [HttpGet]
        public async Task<IActionResult> GetLocationsAsync()
        {
            try
            {
                return Ok(await ILS.GetLocationsAsync());
            }
            catch (LocationDbException)
            {
                return NotFound();
            }
        }
    }
}
