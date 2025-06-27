using backFGRB.Application.Service.Logs;
using backFGRB.Application.Services.Auth;
using backFGRB.Application.Services.CurrentUser;
using backFGRB.Application.Services.Logs;
using backFGRB.Application.Services.Users;
using Microsoft.Extensions.DependencyInjection;

namespace backFGRB.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        //services.AddScoped<Interface, Service>();
        services.AddAutoMapper(typeof(DependencyInjection).Assembly);
        services.AddScoped<ILogService, LogService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}