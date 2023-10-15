using FitNoteIT.Modules.Users.API;
using FitNoteIT.Shared;
using Serilog;

var builder = WebApplication
	.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
	.ReadFrom.Configuration(context.Configuration)
	.ReadFrom.Services(services)
	.Enrich.FromLogContext());

builder.Services
	.AddUsersModule(builder.Configuration)
	.AddSharedFramework(builder.Configuration);

var app = builder.Build();

app.UseUsersModule();
app.UseSharedFramework();

app.MapControllers();
app.MapGet("/", ctx => ctx.Response.WriteAsync("FitNoteIT API"));

app.Run();