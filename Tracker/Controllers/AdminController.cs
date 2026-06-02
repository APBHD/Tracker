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
            return Ok(await _adminService.DeleteEmployee(id));
        }

        [HttpGet("projects")]
        public async Task<IActionResult> GetProjects()
        {
            return Ok(await _adminService.GetAllProjects());
        }

        [HttpPost("project")]
        public async Task<IActionResult> CreateProject(Project project)
        {
            return Ok(await _adminService.CreateProject(project));
        }

        [HttpPost("assign")]
        public async Task<IActionResult> Assign(int userId, int projectId)
        {
            return Ok(await _adminService.AssignEmployeeToProject(userId, projectId));
        }
    }
}