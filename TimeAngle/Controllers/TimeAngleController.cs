using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TimeAngle.Services;

namespace TimeAngle.Controllers
{
    [ApiController]
    [Route("api/timeangle")]
    public class TimeAngleController : ControllerBase
    {
        private readonly ILogger<TimeAngleController> _logger;
        private readonly TimeAngleService _timeAngleService;

        public TimeAngleController(TimeAngleService timeAngleService, ILogger<TimeAngleController> logger)
        {
            _timeAngleService = timeAngleService;
            _logger = logger;
        }

        [HttpGet("calculate-angle")]
        public async Task<IActionResult> CalculcateTimeAngle([FromQuery] int hour, [FromQuery] int minute)
        {
            var angle = await _timeAngleService.CalculateTimeAngle(hour, minute);
            return Ok(angle);
        }

        [HttpPost("calculate-angle")]
        public async Task<IActionResult> CalculcateTimeAngle([FromBody] string time)
        {
            try
            {
                var angle = await _timeAngleService.CalculateTimeAngle(time);
                return Ok(angle);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = "Unexpected server error." });
            }
        }
    }
}
