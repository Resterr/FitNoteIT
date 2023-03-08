using FitNoteIT.Modules.Users.Api;
using FitNoteIT.Shared;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false)
        .Build();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.WithOrigins(config["Cors:AllowedOrigins"].Split(","))
                   .WithMethods(config["Cors:AllowedMethods"].Split(","))
                   .WithHeaders(config["Cors:AllowedHeaders"].Split(","));
        });
});

builder.Services.AddSharedFramework(builder.Configuration);
builder.Services.AddUsersModule(builder.Configuration);

var app = builder.Build();

app.UseSharedFramework();
app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.MapGet("/", ctx => ctx.Response.WriteAsync("FitNoteIT API is ok"));
app.RegisterUsersModuleRequests();

app.Run();