
using System.Text.Json;
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
        var existingUserName = await _userRepository.GetByStringAsync(dto.UserName);
        if (existingUserName is not null)
            throw new Exception("User with this username already exists.");

        var existingEmail = await _userRepository.GetByStringAsync(dto.Email);
        if (existingEmail is not null)
            throw new Exception("User with this email already exists.");

        var user = _mapper.Map<User>(dto);
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var userName = _currentUser.Username ?? "System";
        var ipAddress = _currentRequest.IpAddress ?? "0.0.0.0";

        user.CreatedAt = DateTime.UtcNow;
        user.CreatedBy = userName;

        await _userRepository.Add(user);

        var auditData = _mapper.Map<UserAuditDto>(user);

        await _logService.RegisterAsync(new CreateLogDto
        {
            Action = "Create",
            Resource = "User",
            PerformedBy = userName,
            IpAddress = ipAddress,
            DataBefore = "",
            DataAfter = JsonSerializer.Serialize(auditData, new JsonSerializerOptions
            {
                WriteIndented = true
            })
        });

        return _mapper.Map<UserViewModel>(user);
    }

    public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAll();
        if (!users.Any())
            throw new Exception("Not found users.");

        var userName = _currentUser.Username ?? "System";
        var ipAddress = _currentRequest.IpAddress ?? "0.0.0.0";

        await _logService.RegisterAsync(new CreateLogDto
        {
            Action = "Read",
            Resource = "User",
            PerformedBy = userName,
            IpAddress = ipAddress,
            DataBefore = "",
            DataAfter = ""
        });

        return _mapper.Map<IEnumerable<UserViewModel>>(users);
    }

    public async Task<UserViewModel> GetByStringAsync(string user)
    {
        var existingUser = await _userRepository.GetByStringAsync(user);
        if (existingUser is null)
            throw new Exception("Not found user.");

        var userName = _currentUser.Username ?? "System";
        var ipAddress = _currentRequest.IpAddress ?? "0.0.0.0";

        await _logService.RegisterAsync(new CreateLogDto
        {
            Action = "Read",
            Resource = "User",
            PerformedBy = userName,
            IpAddress = ipAddress,
            DataBefore = "",
            DataAfter = ""
        });

        return _mapper.Map<UserViewModel>(existingUser);
    }

    public async Task<UserViewModel> UpdateAsync(int id, UpdateUserDto dto)
    {
        var existingUser = await _userRepository.GetById(id);
        if (existingUser is null)
            throw new Exception("Not found user.");

        var validateUserName = await _userRepository.GetByStringAsync(dto.UserName);
        if (validateUserName is not null && validateUserName.Id != id)
            throw new Exception("User with this username already exists.");

        var validateEmail = await _userRepository.GetByStringAsync(dto.Email);
        if (validateEmail is not null && validateEmail.Id != id)
            throw new Exception("User with this email already exists.");

        var userName = _currentUser.Username ?? "System";
        var ipAddress = _currentRequest.IpAddress ?? "0.0.0.0";
        var dataBefore = _mapper.Map<UserAuditDto>(existingUser);

        existingUser.UserName = dto.UserName;
        existingUser.Email = dto.Email;
        existingUser.FullName = dto.FullName;
        existingUser.Role = dto.Role;
        existingUser.UpdatedAt = DateTime.UtcNow;
        existingUser.UpdatedBy = userName;

        _userRepository.Update(existingUser);

        var dataAfter = _mapper.Map<UserAuditDto>(existingUser);

        await _logService.RegisterAsync(new CreateLogDto
        {
            Action = "Updated",
            Resource = "User",
            PerformedBy = userName,
            IpAddress = ipAddress,
            DataBefore = JsonSerializer.Serialize(dataBefore, new JsonSerializerOptions
            {
                WriteIndented = true
            }),
            DataAfter = JsonSerializer.Serialize(dataAfter, new JsonSerializerOptions
            {
                WriteIndented = true
            })
        });

        return _mapper.Map<UserViewModel>(existingUser);
    }

    public async Task<bool> DeactiveAsync(int id)
    {
        var existingUser = await _userRepository.GetById(id);
        if (existingUser is null)
            throw new Exception("Not found user.");

        var userName = _currentUser.Username ?? "System";
        var ipAddress = _currentRequest.IpAddress ?? "0.0.0.0";
        var dataBefore = _mapper.Map<UserAuditDto>(existingUser);

        existingUser.Active = false;
        existingUser.DeletedAt = DateTime.UtcNow;
        existingUser.DeletedBy = userName;

        _userRepository.Update(existingUser);

        var dataAfter = _mapper.Map<UserAuditDto>(existingUser);

        await _logService.RegisterAsync(new CreateLogDto
        {
            Action = "SoftDeleted",
            Resource = "User",
            PerformedBy = userName,
            IpAddress = ipAddress,
            DataBefore = JsonSerializer.Serialize(dataBefore, new JsonSerializerOptions
            {
                WriteIndented = true
            }),
            DataAfter = JsonSerializer.Serialize(dataAfter, new JsonSerializerOptions
            {
                WriteIndented = true
            })
        });

        return true;
    }
}