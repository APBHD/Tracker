using Microsoft.EntityFrameworkCore;
using Tracker.Data;
using Tracker.DTOs;
using Tracker.Entities;
using Tracker.Interfaces;

namespace Tracker.Services
{
    public class ProjectService : IProjectService
    {
        private readonly AppDbContext _context;

        public ProjectService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> CreateProject(ProjectCreateDto dto)
        {
            var project = new Project
            {
                ProjectName = dto.ProjectName,
                Description = dto.Description
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return "Project created successfully";
        }

        public async Task<List<Project>> GetAllProjects()
        {
            return await _context.Projects.ToListAsync();
        }
    }
}