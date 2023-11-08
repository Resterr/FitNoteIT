using FitNoteIT.Bootstrapper.Policies;
using FitNoteIT.Modules.Exercises.API;
using FitNoteIT.Modules.Users.API;
using FitNoteIT.Modules.Workouts.API;
using FitNoteIT.Shared;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration.ReadFrom.Configuration(context.Configuration)
	.ReadFrom.Services(services)
	.Enrich.FromLogContext());

builder.Services.AddCorsPolicy();
builder.Services.AddUsersModule(builder.Configuration)
	.AddExercisesModule()
	.AddWorkoutsModule(builder.Configuration)
	.AddSharedFramework(builder.Configuration);

var app = builder.Build();

app.UseCorsPolicy();

app.UseUsersModule();
app.UseExercisesModule();
app.UseSharedFramework();

app.RegisterUsersModuleRequests();
app.RegisterExercisesModuleRequests();
app.RegisterWorkoutsModuleRequests();
app.MapGet("/", ctx => ctx.Response.WriteAsync("FitNoteIT API"));

app.Run();