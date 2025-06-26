using backFGRB.Application.Services.CurrentRequestService;

namespace backFGRB.WebAPI.Services;

public class CurrentRequestService : ICurrentRequestService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentRequestService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? IpAddress =>
        _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
}