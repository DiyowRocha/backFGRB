using backFGRB.Infrastructure.Context;
using backFGRB.Infrastructure.Repositories.Logs;
using backFGRB.Infrastructure.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace backFGRB.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationContext>(options
        => options.UseNpgsql(connectionString));

        //services.AddScoped<Interface, Service>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ILogRepository, LogRepository>();
        
        return services;
    }
}