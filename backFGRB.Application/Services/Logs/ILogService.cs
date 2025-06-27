namespace backFGRB.Application.Services.Logs;

public interface ILogService
{
    Task RegisterAsync(CreateLogDto dto);
    Task<IEnumerable<LogViewModel?>> GetAllAsync();
    Task<IEnumerable<LogViewModel?>> GetLogByUserAsync(GetLogByUserDto dto);
}