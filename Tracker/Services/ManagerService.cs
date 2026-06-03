using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Tracker.Data;
using Tracker.Entities;
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
        public async Task<List<WorkLog>> GetApproved()
        {
            return await _context.WorkLogs
                .Where(x => x.Status == "Approved")
                .Include(x => x.User)
                .Include(x => x.Project)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        public async Task<List<WorkLog>> GetDeclined()
        {
            return await _context.WorkLogs
                .Where(x => x.Status == "Declined")
                .Include(x => x.User)
                .Include(x => x.Project)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }
        public async Task<List<WorkLog>> GetAllWorkLogs()
        {
            return await _context.WorkLogs
                .Include(x => x.User)
                .Include(x => x.Project)
                .ToListAsync();
        }

        public async Task<List<WorkLog>> GetPending()
        {
            return await _context.WorkLogs
                .Where(x => x.Status == "Pending")
                .Include(x => x.User)
                .Include(x => x.Project)
                .ToListAsync();
        }

        public async Task<string> Approve(int workLogId)
        {
            var log = await _context.WorkLogs.FindAsync(workLogId);

            if (log == null)
                return "Not found";

            log.Status = "Approved";

            int rows = await _context.SaveChangesAsync();

            var employee = await _context.Users
                .FirstOrDefaultAsync(x => x.UserId == log.UserId);

            await _hub.Clients.All.SendAsync(
                "ReceiveNotification",
                $"✅ WorkLog approved for {employee?.FullName}"
            );

            return $"Approved. Rows Updated = {rows}";
        }
        public async Task<string> Decline(int workLogId)
        {
            var log = await _context.WorkLogs.FindAsync(workLogId);

            if (log == null)
                return "Not found";

            log.Status = "Declined";
            await _context.SaveChangesAsync();

            var employee = await _context.Users
    .FirstOrDefaultAsync(x => x.UserId == log.UserId);

            await _hub.Clients.All.SendAsync(
                "ReceiveNotification",
                $"❌ WorkLog declined for {employee?.FullName}"
            );

            return "Declined";
        }
    }
}