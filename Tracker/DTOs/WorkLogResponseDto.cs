namespace Tracker.DTOs
{
    public class WorkLogResponseDto
    {
        public int WorkLogId { get; set; }

        public string ProjectName { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal HoursWorked { get; set; }
    }
}