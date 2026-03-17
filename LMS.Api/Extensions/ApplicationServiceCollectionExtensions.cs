namespace LMS.Api.Extensions;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAuthServices();
        services.AddMcpToolsService();
        return services;
    }
}
