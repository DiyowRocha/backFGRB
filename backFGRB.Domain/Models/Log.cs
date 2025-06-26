namespace backFGRB.Domain.Models;

public class Log
{
    public Guid Id { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string Action { get; set; } = string.Empty;
    public string Resource { get; set; } = string.Empty;
    public string PerformedBy { get; set; } = string.Empty;
    public string DataBefore { get; set; } = string.Empty;
    public string DataAfter { get; set; } = string.Empty;
    public string IPAddress { get; set; } = string.Empty;
}