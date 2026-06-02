using Tracker.DTOs;
using Tracker.Entities;

namespace Tracker.Interfaces
{
    public interface IEmployeeService
    {
        Task<string> AddWorkLog(WorkLog log);

        Task<List<WorkLogResponseDto>> GetMyWorkLogs(int userId);

        Task<List<Project>> GetMyProjects(int userId);
    }
}
