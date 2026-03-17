using LMS.Api.MCPServer;

namespace LMS.Api.Extensions;

public static class McpServerServiceCollectionExtensions
{
    public static IServiceCollection AddMcpToolsService(this IServiceCollection services)
    {
        services.AddScoped<CourseTools>();
        return services;
    }
}
