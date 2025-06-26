using Microsoft.Extensions.DependencyInjection;

namespace backFGRB.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        //services.AddScoped<Interface, Service>();
        services.AddAutoMapper(typeof(DependencyInjection).Assembly);

        return services;
    }
}