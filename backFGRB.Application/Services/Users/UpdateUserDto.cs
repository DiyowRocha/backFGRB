using backFGRB.Domain.Enums;

namespace backFGRB.Application.Services.Users;

public class UpdateUserDto
{
    public string UserName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public UserRole Role { get; set; }
}