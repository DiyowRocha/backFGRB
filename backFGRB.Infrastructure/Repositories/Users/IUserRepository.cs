using backFGRB.Domain.Models;
using backFGRB.Infrastructure.Repositories.BaseRepository;

namespace backFGRB.Infrastructure.Repositories.Users;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByEmailOrUserNameAsync(string email, string username);
}