namespace Tracker.DTOs
{
    public record LoginResult(bool Success, string? Token = null, string? Error = null);
}
