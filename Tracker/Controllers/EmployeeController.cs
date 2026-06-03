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
            return int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );
        }

        // POST WORKLOG
        [HttpPost("worklog")]
        public async Task<IActionResult> AddWorklog(WorkLogCreateDto dto)
        {
            var log = new WorkLog
            {
                UserId = GetUserId(),
                ProjectId = dto.ProjectId,
                Date = dto.Date,
                TaskDescription = dto.TaskDescription,
                HoursWorked = dto.HoursWorked,
                WfhRequest = dto.WfhRequest,
                PermissionHours = dto.PermissionHours
            };

            var result = await _service.AddWorkLog(log);

            return Ok(result);
        }

        // EMPLOYEE DASHBOARD
        [HttpGet("dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            var userId = GetUserId();

            var result = await _service.GetDashboard(userId);

            return Ok(result);
        }

        // GET MY WORKLOGS
        [HttpGet("worklogs")]
        public async Task<IActionResult> GetWorklogs()
        {
            var userId = GetUserId();

            var result = await _service.GetMyWorkLogs(userId);

            return Ok(result);
        }
    }
}