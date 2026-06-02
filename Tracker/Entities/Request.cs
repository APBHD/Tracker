namespace Tracker.Entities
{
    public class Request
    {
        public int RequestId { get; set; }
        public int EmployeeId { get; set; }
        public string RequestType { get; set; } = string.Empty;
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
    }
}