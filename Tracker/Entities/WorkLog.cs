using Tracker.Entities;

public class WorkLog
{
    public int WorkLogId { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public int ProjectId { get; set; }
    public Project Project { get; set; }

    public DateTime Date { get; set; }

    public string Description { get; set; }

    public decimal HoursWorked { get; set; }

    public string Status { get; set; } = "Pending"; // Pending / Approved / Declined
}