using FitNoteIT.Bootstrapper.Policies;
using FitNoteIT.Modules.Users.API;
using FitNoteIT.Shared;
using Serilog;

var builder = WebApplication
	.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
	.ReadFrom.Configuration(context.Configuration)
	.ReadFrom.Services(services)
	.Enrich.FromLogContext());

builder.Services.AddCorsPolicy();
builder.Services
	.AddUsersModule(builder.Configuration)
	.AddSharedFramework(builder.Configuration);

var app = builder.Build();

app.UseCorsPolicy();
app.UseUsersModule();
app.UseSharedFramework();

app.RegisterUsersModuleRequests();
app.MapGet("/", ctx => ctx.Response.WriteAsync("FitNoteIT API"));

app.Run();