using System.ComponentModel.DataAnnotations;

namespace Tracker.Entities
{
    public class EmpProject
    {
        [Key]
        public int EmpProjectId { get; set; }   

        public int UserId { get; set; }

        public int ProjectId { get; set; }

        public Project? Project { get; set; }

        public User? User { get; set; }
    }
}