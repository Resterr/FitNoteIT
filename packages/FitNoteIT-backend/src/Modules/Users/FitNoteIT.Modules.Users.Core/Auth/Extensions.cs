using FitNoteIT.Shared.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FitNoteIT.Modules.Users.Core.Auth;
internal static class Extensions
{
    private const string _optionsSectionName = "Auth";

    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetOptions<AuthOptions>(_optionsSectionName);

        services
            .Configure<AuthOptions>(configuration.GetRequiredSection(_optionsSectionName))
            .AddSingleton<IAuthenticator, Authenticator>()
            .AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.Audience = options.Audience;
                o.IncludeErrorDetails = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = options.Issuer,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigningKey))
                };
            });

        services.AddAuthorization(authorization =>
        {
            authorization.AddPolicy("is-superadmin", policy =>
            {
                policy.RequireRole("SuperAdmin");
            });

            authorization.AddPolicy("is-admin", policy =>
            {
                policy.RequireRole("Admin");
            });

            authorization.AddPolicy("is-user", policy =>
            {
                policy.RequireRole("User");
            });
        });

        return services;
    }
}
