namespace Tracker.DTOs
{
    public class EmployeeDashboardDto
    {
        public string ProjectName { get; set; } = string.Empty;

        public decimal TotalHours { get; set; }

        public int TotalWorkLogs { get; set; }

        public int ApprovedCount { get; set; }

        public int PendingCount { get; set; }

        public int DeclinedCount { get; set; }
    }
}