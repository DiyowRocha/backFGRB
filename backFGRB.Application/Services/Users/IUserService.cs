namespace backFGRB.Application.Services.Users;

public interface IUserService
{
    Task<UserViewModel> CreateAsync(CreateUserDto dto);
}