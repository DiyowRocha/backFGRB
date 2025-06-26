using backFGRB.Domain.Models;
using backFGRB.Infrastructure.Context;
using backFGRB.Infrastructure.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace backFGRB.Infrastructure.Repositories.Logs;

public class LogRepository : BaseRepository<Log>, ILogRepository
{
    public LogRepository(ApplicationContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Log>> GetByUser(string user)
    {
        return await _context.Logs
            .Where(l => l.PerformedBy == user)
            .OrderByDescending(l => l.Timestamp)
            .ToListAsync();
    }
}