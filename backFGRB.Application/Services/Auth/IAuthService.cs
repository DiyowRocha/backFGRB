namespace backFGRB.Application.Services.Auth;

public interface IAuthService
{
    Task<AuthViewModel> LoginAsync(LoginDto dto);
}