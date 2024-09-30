using Identity_Application.Common.Interfaces.DbContexts;
using Identity_Application.Common.Interfaces.Services;
using Identity_Domain.Entities;
using Identity_Infrastructure.Account.Services;
using Identity_Infrastructure.Identity;
using Identity_Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Identity_Infrastructure;

public static class DependencyInitializer
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configurations)
    {

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            string dbConnectionString = configurations.GetConnectionString("DefaultConnection");
            if (configurations["UseInMemoryDatabase"] == "true")
                options.UseInMemoryDatabase("testDB");
            else
                options.UseSqlServer(dbConnectionString);
        });

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddScoped<ApplicationDbContextInitializer>();

        services.AddScoped<DbMigration>();
        services.AddHttpClient();

        services.AddScoped<IUserHttpContextAccessor, UserHttpContextAccessor>();

        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.ClaimsIdentity.EmailClaimType = Identity_Application.ClaimTypes.Email;
            options.ClaimsIdentity.RoleClaimType = Identity_Application.ClaimTypes.Role;
            options.ClaimsIdentity.UserIdClaimType = Identity_Application.ClaimTypes.Id;
            options.ClaimsIdentity.UserNameClaimType = Identity_Application.ClaimTypes.Username;
            options.User.RequireUniqueEmail = false;
        })
       .AddEntityFrameworkStores<ApplicationDbContext>()
       .AddDefaultTokenProviders();



        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        services.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(op =>
        {
            op.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configurations["JWT:Secret"])),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
      
        services.AddAuthorization(auth =>
        {
            auth.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();

            auth.AddPolicy(ApplicationPolicies.SuperAdministrator, op => op
                .RequireRole(ApplicationClaimValues.SuperAdministrator));

            auth.AddPolicy(ApplicationPolicies.Administrator, op => op
                .RequireRole(ApplicationClaimValues.SuperAdministrator, ApplicationClaimValues.Administrator));
        });
        var corsAllowedHosts = configurations.GetSection("CORS").Get<string[]>();
        services.AddCors(options =>
        {
            options.AddPolicy("CORS_POLICY", policyConfig =>
            {
                policyConfig.WithOrigins(corsAllowedHosts)
                            .AllowAnyHeader()
                            .AllowAnyMethod();
            });
        });

    }


}
