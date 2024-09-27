using FluentValidation;
using MediatR;
using Identity_Application.Common.Interfaces.Services;
using Identity_Application.Features.Users;
using Identity_Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Identity_Application;

public static class DependencyInitializer
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddOptions();
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddAutoMapper(typeof(UsersProfile).Assembly);

        Assembly assem = Assembly.GetExecutingAssembly();
        services.AddValidatorsFromAssembly(assem);

        services.AddScoped<IJwtTokenService, JwtTokenService>();
    }
}