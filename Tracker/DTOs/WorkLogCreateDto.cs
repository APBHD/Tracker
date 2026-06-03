namespace Tracker.DTOs
{
    public class WorkLogCreateDto
    {
        public int ProjectId { get; set; }

        public DateTime Date { get; set; }

        public string TaskDescription { get; set; } = string.Empty;

        public decimal HoursWorked { get; set; }

        public bool WfhRequest { get; set; }

        public int PermissionHours { get; set; }
    }
}