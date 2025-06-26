namespace backFGRB.Application.Services.Logs;

public class CreateLogDto
{
    public string Action { get; set; } = string.Empty;
    public string Resource { get; set; } = string.Empty;
    public string DateAfter { get; set; } = string.Empty;
    public string DateBefore { get; set; } = string.Empty;
    public string PerformedBy { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
}