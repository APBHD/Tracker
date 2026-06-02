using Tracker.Entities;

namespace Tracker.Interfaces
{
    public interface IAdminService
    {
        Task<List<User>> GetAllEmployees();
        Task<string> DeleteEmployee(int userId);

        Task<List<Project>> GetAllProjects();
        Task<string> CreateProject(Project project);

        Task<string> AssignEmployeeToProject(int userId, int projectId);
    }
}