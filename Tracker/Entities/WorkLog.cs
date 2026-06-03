using Tracker.Entities;

public class WorkLog
{
    public int WorkLogId { get; set; }
    public int UserId { get; set; }
    public int ProjectId { get; set; }
    public DateTime Date { get; set; }

    public string TaskDescription { get; set; } = string.Empty;

    public decimal HoursWorked { get; set; }
    public bool WfhRequest { get; set; }

    public int PermissionHours { get; set; }

    public string Status { get; set; } = "Pending";

    public User? User { get; set; }

    public Project? Project { get; set; }
}