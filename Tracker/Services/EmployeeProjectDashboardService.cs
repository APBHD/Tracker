using Microsoft.EntityFrameworkCore;
using Tracker.Data;
using Tracker.DTOs;

namespace Tracker.Services
{
    public class EmployeeProjectDashboardService
    {
        private readonly AppDbContext _context;

        public EmployeeProjectDashboardService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<EmployeeProjectDashboardDto> GetEmployeeProjectDashboard()
        {
            var workLogs = await _context.WorkLogs
                .Include(x => x.User)
                .Include(x => x.Project)
                .ToListAsync();

            return new EmployeeProjectDashboardDto
            {
                TotalEmployees = await _context.Users.CountAsync(),
                TotalProjects = await _context.Projects.CountAsync(),

                EmployeeSummaries = workLogs
                    .GroupBy(x => x.UserId)
                    .Select(g => new EmployeeProjectSummary
                    {
                        UserId = g.Key,
                        EmployeeName = g.First().User?.FullName ?? "Unknown",
                        TotalProjects = g.Select(x => x.ProjectId).Distinct().Count(),
                        TotalWorkLogs = g.Count(),
                        TotalHours = g.Sum(x => x.HoursWorked)
                    })
                    .ToList()
            };
        }
    }
}
