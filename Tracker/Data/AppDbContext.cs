using Microsoft.EntityFrameworkCore;
using Tracker.Entities;

namespace Tracker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }   

        public DbSet<Project> Projects { get; set; }

        public DbSet<WorkLog> WorkLogs { get; set; }

        public DbSet<EmpProject> EmpProjects { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkLog>()
                .Property(x => x.HoursWorked)
                .HasPrecision(5, 2);
        }

    }
}