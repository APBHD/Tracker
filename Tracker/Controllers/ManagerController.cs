using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tracker.Services;

namespace Tracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Manager")]
    public class ManagerController : ControllerBase
    {
        private readonly ManagerService _service;

        public ManagerController(ManagerService service)
        {
            _service = service;
        }

        [HttpGet("worklogs")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllWorkLogs());
        }

        [HttpGet("pending")]
        public async Task<IActionResult> Pending()
        {
            return Ok(await _service.GetPending());
        }

        [HttpGet("approved")]
        public async Task<IActionResult> GetApproved()
        {
            return Ok(await _service.GetApproved());
        }

        [HttpGet("declined")]
        public async Task<IActionResult> GetDeclined()
        {
            return Ok(await _service.GetDeclined());
        }

        [HttpPut("approve/{id}")]
        public async Task<IActionResult> Approve(int id)
        {
            var result = await _service.Approve(id);
            if (result == null)
                return NotFound(new { message = "WorkLog not found" });
            return Ok(new { message = result });
        }

        [HttpPut("decline/{id}")]
        public async Task<IActionResult> Decline(int id)
        {
            var result = await _service.Decline(id);
            if (result == null)
                return NotFound(new { message = "WorkLog not found" });
            return Ok(new { message = result });
        }
    }
}
