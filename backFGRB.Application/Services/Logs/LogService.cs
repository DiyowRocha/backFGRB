using AutoMapper;
using backFGRB.Application.Services.Logs;
using backFGRB.Domain.Models;
using backFGRB.Infrastructure.Repositories.Logs;

namespace backFGRB.Application.Service.Logs;

public class LogService : ILogService
{
    private readonly ILogRepository _logRepository;
    private readonly IMapper _mapper;

    public LogService(ILogRepository logRepository, IMapper mapper)
    {
        _logRepository = logRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<LogViewModel?>> GetAllAsync()
    {
        var logs = await _logRepository.GetAll();
        return _mapper.Map<IEnumerable<LogViewModel>>(logs);
    }

    public async Task<IEnumerable<LogViewModel?>> GetLogByUserAsync(string user)
    {
        var logs = await _logRepository.GetByUser(user);
        return _mapper.Map<IEnumerable<LogViewModel>>(logs);
    }

    public async Task RegisterAsync(CreateLogDto dto)
    {
        var log = _mapper.Map<Log>(dto);
        log.Id = Guid.NewGuid();
        log.Timestamp = DateTime.UtcNow;

        await _logRepository.Add(log);
    }
}