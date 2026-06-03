using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Tracker.Data;
using Tracker.DTOs;
using Tracker.Entities;
using Tracker.Hubs;
using Tracker.Interfaces;

namespace Tracker.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<NotificationHub> _hub;

        public EmployeeService(
            AppDbContext context,
            IHubContext<NotificationHub> hub)
        {
            _context = context;
            _hub = hub;
        }

        // =========================
        // ADD WORKLOG
        // =========================
        public async Task<string> AddWorkLog(WorkLog log)
        {
            var projectExists = await _context.Projects
                .AnyAsync(x => x.ProjectId == log.ProjectId);

            if (!projectExists)
                return "Invalid ProjectId";

            log.Status = "Pending";

            _context.WorkLogs.Add(log);

            await _context.SaveChangesAsync();

            await _hub.Clients.All.SendAsync(
                "ReceiveNotification",
                $"📝 New WorkLog submitted by User {log.UserId}"
            );

            return "WorkLog submitted successfully";
        }

        // =========================
        // GET MY WORKLOGS
        // =========================
        public async Task<List<WorkLog>> GetMyWorkLogs(int userId)
        {
            return await _context.WorkLogs
                .Include(x => x.Project)
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        // =========================
        // GET MY PROJECTS
        // =========================
        public async Task<List<Project>> GetMyProjects(int userId)
        {
            return await _context.EmpProjects
                .Where(x => x.UserId == userId)
                .Include(x => x.Project)
                .Select(x => x.Project!)
                .ToListAsync();
        }

        // =========================
        // EMPLOYEE DASHBOARD
        // =========================
        public async Task<List<EmployeeDashboardDto>> GetDashboard(int userId)
        {
            return await _context.WorkLogs
                .Include(x => x.Project)
                .Where(x => x.UserId == userId)
                .GroupBy(x => new
                {
                    x.ProjectId,
                    x.Project!.ProjectName
                })
                .Select(g => new EmployeeDashboardDto
                {
                    ProjectName = g.Key.ProjectName,

                    TotalHours = g.Sum(x => x.HoursWorked),

                    TotalWorkLogs = g.Count(),

                    ApprovedCount = g.Count(x => x.Status == "Approved"),

                    PendingCount = g.Count(x => x.Status == "Pending"),

                    DeclinedCount = g.Count(x => x.Status == "Declined")
                })
                .ToListAsync();
        }
    }
}