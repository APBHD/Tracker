using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Tracker.Data;
using Tracker.DTOs;
using Tracker.Hubs;

namespace Tracker.Services
{
    public class ManagerService
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<NotificationHub> _hub;

        public ManagerService(AppDbContext context, IHubContext<NotificationHub> hub)
        {
            _context = context;
            _hub = hub;
        }

        public async Task<List<WorkLogResponseDto>> GetAllWorkLogs()
        {
            return await _context.WorkLogs
                .OrderByDescending(x => x.Date)
                .Select(x => new WorkLogResponseDto
                {
                    WorkLogId = x.WorkLogId,
                    EmployeeName = x.User!.FullName,
                    ProjectName = x.Project!.ProjectName,
                    Date = x.Date,
                    Description = x.Description,
                    HoursWorked = x.HoursWorked,
                    Status = x.Status
                })
                .ToListAsync();
        }

        public async Task<List<WorkLogResponseDto>> GetPending()
        {
            return await _context.WorkLogs
                .Where(x => x.Status == "Pending")
                .OrderByDescending(x => x.Date)
                .Select(x => new WorkLogResponseDto
                {
                    WorkLogId = x.WorkLogId,
                    EmployeeName = x.User!.FullName,
                    ProjectName = x.Project!.ProjectName,
                    Date = x.Date,
                    Description = x.Description,
                    HoursWorked = x.HoursWorked,
                    Status = x.Status
                })
                .ToListAsync();
        }

        public async Task<List<WorkLogResponseDto>> GetApproved()
        {
            return await _context.WorkLogs
                .Where(x => x.Status == "Approved")
                .OrderByDescending(x => x.Date)
                .Select(x => new WorkLogResponseDto
                {
                    WorkLogId = x.WorkLogId,
                    EmployeeName = x.User!.FullName,
                    ProjectName = x.Project!.ProjectName,
                    Date = x.Date,
                    Description = x.Description,
                    HoursWorked = x.HoursWorked,
                    Status = x.Status
                })
                .ToListAsync();
        }

        public async Task<List<WorkLogResponseDto>> GetDeclined()
        {
            return await _context.WorkLogs
                .Where(x => x.Status == "Declined")
                .OrderByDescending(x => x.Date)
                .Select(x => new WorkLogResponseDto
                {
                    WorkLogId = x.WorkLogId,
                    EmployeeName = x.User!.FullName,
                    ProjectName = x.Project!.ProjectName,
                    Date = x.Date,
                    Description = x.Description,
                    HoursWorked = x.HoursWorked,
                    Status = x.Status
                })
                .ToListAsync();
        }

        public async Task<string?> Approve(int workLogId)
        {
            var log = await _context.WorkLogs.FindAsync(workLogId);

            if (log == null)
                return null;

            log.Status = "Approved";
            await _context.SaveChangesAsync();

            await _hub.Clients.All.SendAsync("ReceiveNotification", $"WorkLog {workLogId} Approved");

            return "Approved";
        }

        public async Task<string?> Decline(int workLogId)
        {
            var log = await _context.WorkLogs.FindAsync(workLogId);

            if (log == null)
                return null;

            log.Status = "Declined";
            await _context.SaveChangesAsync();

            await _hub.Clients.All.SendAsync("ReceiveNotification", $"WorkLog {workLogId} Declined");

            return "Declined";
        }
    }
}
