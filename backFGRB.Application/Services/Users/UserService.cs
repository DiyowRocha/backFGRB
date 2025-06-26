
using AutoMapper;
using backFGRB.Application.Services.CurrentRequestService;
using backFGRB.Application.Services.CurrentUser;
using backFGRB.Application.Services.Logs;
using backFGRB.Domain.Models;
using backFGRB.Infrastructure.Repositories.Users;

namespace backFGRB.Application.Services.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogService _logService;
    private readonly ICurrentUserService _currentUser;
    private readonly ICurrentRequestService _currentRequest;

    public UserService(IUserRepository userRepository, IMapper mapper, ILogService logService, ICurrentUserService currentUser, ICurrentRequestService currentRequest)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _logService = logService;
        _currentUser = currentUser;
        _currentRequest = currentRequest;
    }

    public async Task<UserViewModel> CreateAsync(CreateUserDto dto)
    {
        var existing = await _userRepository.GetByEmailOrUserNameAsync(dto.Email, dto.UserName);
        if (existing is not null)
            throw new Exception("There is already a user with that email or username.");

        var user = _mapper.Map<User>(dto);
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var userName = _currentUser.Username ?? "System";
        var ipAddress = _currentRequest.IpAddress ?? "0.0.0.0";

        user.CreatedAt = DateTime.UtcNow;
        user.CreatedBy = userName;

        await _userRepository.Add(user);

        await _logService.RegisterAsync(new CreateLogDto
        {
            Action = "Create",
            Resource = "User",
            PerformedBy = userName,
            IpAddress = ipAddress,
            DateAfter = "",
            DateBefore = System.Text.Json.JsonSerializer.Serialize(user)
        });

        return _mapper.Map<UserViewModel>(user);
    }
}