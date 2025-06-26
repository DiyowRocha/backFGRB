namespace backFGRB.Application.Services.Logs;

public class LogViewModel
{
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string Action { get; set; } = string.Empty;
    public string Resource { get; set; } = string.Empty;
    public string PerformedBy { get; set; } = string.Empty;
    public string DataBefore { get; set; } = string.Empty;
    public string DataAfter { get; set; } = string.Empty;
    public string IPAddress { get; set; } = string.Empty;
}