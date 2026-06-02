using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tracker.DTOs;
using Tracker.Interfaces;

namespace Tracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _service;

        public ProjectController(IProjectService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectCreateDto dto)
        {
            return Ok(await _service.CreateProject(dto));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllProjects());
        }
    }
}