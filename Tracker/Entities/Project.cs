namespace Tracker.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Status { get; set; } = "Active";
    }
}