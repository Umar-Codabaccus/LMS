using Microsoft.AspNetCore.OutputCaching.StackExchangeRedis;

namespace LMS.Api.Extensions;

public static class OutputCacheServiceCollectionExtensions
{
    public static IServiceCollection AddOutputCachePolicies(this IServiceCollection services)
    {
        services.AddOutputCache(options =>
        {
            options.AddPolicy("EnrolledCoursesPolicy", policy => 
                policy
                    .Expire(TimeSpan.FromMinutes(5))
                    
            );
        });
        return services;
    }
}