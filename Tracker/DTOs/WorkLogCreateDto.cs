namespace Tracker.DTOs
{
    public class WorkLogCreateDto
    {
        public int ProjectId { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal HoursWorked { get; set; }
    }
}