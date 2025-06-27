using backFGRB.Domain.Models;
using backFGRB.Infrastructure.Context;
using backFGRB.Infrastructure.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace backFGRB.Infrastructure.Repositories.Users;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ApplicationContext context) : base(context)
    {
    }

    public async Task<User?> GetByStringAsync(string user)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u =>
                u.Email == user ||
                u.UserName == user ||
                u.FullName.ToLower().Contains(user.ToLower()));
    }
}