using Microsoft.EntityFrameworkCore;
using Tracker.Data;
using Tracker.DTOs;
using Tracker.Entities;
using Tracker.Interfaces;

namespace Tracker.Services
{
    public class AdminService : IAdminService
    {
        private readonly AppDbContext _context;

        public AdminService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserResponseDto>> GetAllEmployees()
        {
            return await _context.Users
                .Where(x => x.Role == "Employee" && x.IsActive)
                .Select(u => new UserResponseDto
                {
                    UserId = u.UserId,
                    FullName = u.FullName,
                    Email = u.Email,
                    Role = u.Role,
                    IsActive = u.IsActive
                })
                .ToListAsync();
        }

        public async Task<string?> DeleteEmployee(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                return null;

            user.IsActive = false;
            await _context.SaveChangesAsync();

            return "Employee deleted successfully";
        }

        public async Task<List<Project>> GetAllProjects()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<string> CreateProject(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return "Project created successfully";
        }

        public async Task<string> AssignEmployeeToProject(int userId, int projectId)
        {
            var exists = await _context.EmpProjects
                .AnyAsync(x => x.UserId == userId && x.ProjectId == projectId);

            if (exists)
                return "Already assigned";

            var mapping = new EmpProject
            {
                UserId = userId,
                ProjectId = projectId
            };

            _context.EmpProjects.Add(mapping);
            await _context.SaveChangesAsync();

            return "Employee assigned successfully";
        }
    }
}
