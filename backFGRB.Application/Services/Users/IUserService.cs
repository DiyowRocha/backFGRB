namespace backFGRB.Application.Services.Users;

public interface IUserService
{
    Task<UserViewModel> CreateAsync(CreateUserDto dto);
    Task<IEnumerable<UserViewModel>> GetAllUsersAsync();
    Task<UserViewModel> GetByStringAsync(string user);
    Task<UserViewModel> UpdateAsync(int id, UpdateUserDto dto);
    Task<bool> DeactiveAsync(int id);
}