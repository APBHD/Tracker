using System.ComponentModel.DataAnnotations;

namespace Tracker.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = "Employee";
        // Roles: Admin / Manager / Employee

        public int? ManagerId { get; set; }  // for team structure

        public bool IsActive { get; set; } = true;
    }
}