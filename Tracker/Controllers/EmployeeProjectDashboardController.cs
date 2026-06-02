using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tracker.Services;

namespace Tracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Manager")]
    public class EmployeeProjectDashboardController : ControllerBase
    {
        private readonly EmployeeProjectDashboardService _service;

        public EmployeeProjectDashboardController(EmployeeProjectDashboardService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetEmployeeProjectDashboard());
        }
    }
}
