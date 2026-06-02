namespace Tracker.DTOs
{
    public class EmployeeProjectDashboardDto
    {
        public int TotalEmployees { get; set; }
        public int TotalProjects { get; set; }

        public List<EmployeeProjectSummary> EmployeeSummaries { get; set; }
    }

    public class EmployeeProjectSummary
    {
        public int UserId { get; set; }
        public string EmployeeName { get; set; }

        public int TotalProjects { get; set; }
        public int TotalWorkLogs { get; set; }
        public decimal TotalHours { get; set; }
    }
}