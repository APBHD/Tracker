namespace Tracker.DTOs
{
    public class WorkLogResponseDto
    {
        public int WorkLogId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string ProjectName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal HoursWorked { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
