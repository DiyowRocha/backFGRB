using backFGRB.Domain.Enums;

namespace backFGRB.Domain.Models;

public class User : AuditableEntity
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public bool Active { get; set; }
    
    public UserRole Role { get; set; }
}