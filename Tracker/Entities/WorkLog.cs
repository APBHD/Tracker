namespace Tracker.Entities
{
    public class WorkLog
    {
        public int WorkLogId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        public DateTime Date { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal HoursWorked { get; set; }

        public string Status { get; set; } = "Pending"; // Pending / Approved / Declined
    }
}
