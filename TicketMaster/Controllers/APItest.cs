using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TicketMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APItest : ControllerBase
    {
        [HttpGet]
        public IActionResult GetExample() 
        {
            return Ok(new { Message = "API test complete, and OK" });
        }
    }
}
