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

        // GET ALL WORKLOGS
        [HttpGet("worklogs")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllWorkLogs());
        }

        // GET PENDING ONLY
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

        // APPROVE
        [HttpPut("approve/{id}")]
        public async Task<IActionResult> Approve(int id)
        {
            return Ok(await _service.Approve(id));
        }

        // DECLINE
        [HttpPut("decline/{id}")]
        public async Task<IActionResult> Decline(int id)
        {
            return Ok(await _service.Decline(id));
        }
    }
}