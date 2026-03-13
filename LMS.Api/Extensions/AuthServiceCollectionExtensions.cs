using LMS.Api.Application.AuthServices.Login;
using LMS.Api.Application.AuthServices.Register;

namespace LMS.Api.Extensions;

public static class AuthServiceCollectionExtensions
{
    public static IServiceCollection AddAuthServices(this IServiceCollection services)
    {
        services.AddScoped<ILoginHandler, LoginHandler>();
        services.AddScoped<IRegisterUserHandler, RegisterUserHandler>();
        return services;
    }
}
