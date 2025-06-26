using backFGRB.Domain.Models;
using backFGRB.Infrastructure.Repositories.BaseRepository;

namespace backFGRB.Infrastructure.Repositories.Logs;

public interface ILogRepository : IBaseRepository<Log>
{
    Task<IEnumerable<Log>> GetByUser(string user);
}