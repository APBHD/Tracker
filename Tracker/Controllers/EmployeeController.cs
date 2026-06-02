using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tracker.DTOs;
using Tracker.Entities;
using Tracker.Interfaces;

namespace Tracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }

        [HttpPost("worklog")]
        public async Task<IActionResult> AddWorklog(WorkLogCreateDto dto)
        {
            var log = new WorkLog
            {
                UserId = GetUserId(),
                ProjectId = dto.ProjectId,
                Date = dto.Date,
                Description = dto.Description,
                HoursWorked = dto.HoursWorked
            };

            var result = await _service.AddWorkLog(log);

            if (result != "WorkLog submitted successfully")
                return BadRequest(new { message = result });

            return Ok(new { message = result });
        }

        [HttpGet("worklogs")]
        public async Task<IActionResult> GetWorklogs()
        {
            return Ok(await _service.GetMyWorkLogs(GetUserId()));
        }

        [HttpGet("projects")]
        public async Task<IActionResult> GetMyProjects()
        {
            return Ok(await _service.GetMyProjects(GetUserId()));
        }
    }
}
