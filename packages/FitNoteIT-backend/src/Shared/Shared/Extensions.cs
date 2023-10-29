using FitNoteIT.Shared.Commands;
using FitNoteIT.Shared.Database;
using FitNoteIT.Shared.Dispatchers;
using FitNoteIT.Shared.Events;
using FitNoteIT.Shared.Exceptions;
using FitNoteIT.Shared.Messaging;
using FitNoteIT.Shared.Queries;
using FitNoteIT.Shared.Services;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace FitNoteIT.Shared;

public static class Extensions
{
	private const string _apiTitle = "FitNoteIT API";
	private const string _apiVersion = "v1";

	public static IServiceCollection AddSharedFramework(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddErrorHandling();
		services.AddCommands();
		services.AddEvents();
		services.AddQueries();
		services.AddMessaging();
		services.AddSqlServer(configuration);
		services.AddSingleton<IDateTimeService, DateTimeService>();
		services.AddSingleton<ICurrentUserService, CurrentUserService>();
		services.AddSingleton<IDispatcher, InMemoryDispatcher>();
		services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
		services.AddRouting(options => options.LowercaseUrls = true);
		ValidatorOptions.Global.LanguageManager.Enabled = false;
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen(swagger =>
		{
			swagger.EnableAnnotations();
			swagger.CustomSchemaIds(x => x.FullName);
			swagger.SwaggerDoc(_apiVersion, new OpenApiInfo
			{
				Title = _apiTitle,
				Version = _apiVersion
			});
		});

		return services;
	}

	public static IApplicationBuilder UseSharedFramework(this IApplicationBuilder app)
	{
		app.UseAuthentication();
		app.UseRouting();
		app.UseAuthorization();
		app.UseErrorHandling();
		app.UseHttpsRedirection();
		app.UseSwagger();
		app.UseSwaggerUI(swagger =>
		{
			swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "FitNoteIT");
		});
		app.UseReDoc(reDoc =>
		{
			reDoc.RoutePrefix = "docs";
			reDoc.SpecUrl($"/swagger/{_apiVersion}/swagger.json");
			reDoc.DocumentTitle = _apiTitle;
		});

		return app;
	}
}