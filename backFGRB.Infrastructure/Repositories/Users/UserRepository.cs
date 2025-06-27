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

    public async Task<User?> GetByEmailOrUserNameAsync(string email, string username)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email || u.UserName == username);
    }

    public async Task<User?> GetByLoginAsync(string login)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == login || u.UserName == login);
    }
}