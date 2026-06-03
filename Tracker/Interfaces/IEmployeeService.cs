using Tracker.DTOs;
using Tracker.Entities;

namespace Tracker.Interfaces
{
    public interface IEmployeeService
    {
        Task<string> AddWorkLog(WorkLog log);

        Task<List<WorkLog>> GetMyWorkLogs(int userId);

        Task<List<Project>> GetMyProjects(int userId);

        Task<List<EmployeeDashboardDto>> GetDashboard(int userId);
    }
}