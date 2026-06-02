using Tracker.DTOs;
using Tracker.Entities;

namespace Tracker.Interfaces
{
    public interface IProjectService
    {
        Task<string> CreateProject(ProjectCreateDto dto);

        Task<List<Project>> GetAllProjects();
    }
}