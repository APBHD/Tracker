using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tracker.Entities;
using Tracker.Interfaces;

namespace Tracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("employees")]
        public async Task<IActionResult> GetEmployees()
        {
            return Ok(await _adminService.GetAllEmployees());
        }

        [HttpDelete("employee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _adminService.DeleteEmployee(id);
            if (result == null)
                return NotFound(new { message = "User not found" });
            return Ok(new { message = result });
        }

        [HttpGet("projects")]
        public async Task<IActionResult> GetProjects()
        {
            return Ok(await _adminService.GetAllProjects());
        }

        [HttpPost("project")]
        public async Task<IActionResult> CreateProject(Project project)
        {
            return Ok(new { message = await _adminService.CreateProject(project) });
        }

        [HttpPost("assign")]
        public async Task<IActionResult> Assign([FromQuery] int userId, [FromQuery] int projectId)
        {
            var result = await _adminService.AssignEmployeeToProject(userId, projectId);
            if (result == "Already assigned")
                return Conflict(new { message = result });
            return Ok(new { message = result });
        }
    }
}
