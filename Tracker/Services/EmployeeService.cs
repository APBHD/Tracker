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

        public EmployeeService(AppDbContext context, IHubContext<NotificationHub> hub)
        {
            _context = context;
            _hub = hub;
        }

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
                $"New WorkLog submitted by User {log.UserId}"
            );

            return "WorkLog submitted successfully";
        }

        public async Task<List<WorkLogResponseDto>> GetMyWorkLogs(int userId)
        {
            return await _context.WorkLogs
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.Date)
                .Select(x => new WorkLogResponseDto
                {
                    WorkLogId = x.WorkLogId,
                    ProjectName = x.Project!.ProjectName,
                    Date = x.Date,
                    Description = x.Description,
                    HoursWorked = x.HoursWorked,
                    Status = x.Status
                })
                .ToListAsync();
        }

        public async Task<List<Project>> GetMyProjects(int userId)
        {
            return await _context.EmpProjects
                .Where(x => x.UserId == userId)
                .Select(x => x.Project!)
                .ToListAsync();
        }
    }
}
