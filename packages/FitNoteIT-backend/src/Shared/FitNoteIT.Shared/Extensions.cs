using FitNoteIT.Shared.Auth;
using FitNoteIT.Shared.Exceptions;
using FitNoteIT.Shared.PipelineBehaviours;
using FitNoteIT.Shared.Services;
using FitNoteIT.Shared.Time;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace FitNoteIT.Shared;
public static class Extensions
{
    public static IServiceCollection AddSharedFramework(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddErrorHandling();
        services.AddAuthorization();
        services.AddHttpContextAccessor();
        
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        ValidatorOptions.Global.LanguageManager.Enabled = false;

        services.AddAuth(configuration);
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddSingleton<IClock, Clock>();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(swagger =>
        {
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "FitNoteIT",
                Version = "1.0"
            });
        });

        return services;
    }

    public static IApplicationBuilder UseSharedFramework(this IApplicationBuilder app)
    {
        app.UseErrorHandling();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
}
