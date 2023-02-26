using FitNoteIT.Modules.Users.Api;
using FitNoteIT.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSharedFramework();
builder.Services.AddUsersModule(builder.Configuration);

var app = builder.Build();

app.UseSharedFramework();
app.UseHttpsRedirection();
app.MapGet("/", ctx => ctx.Response.WriteAsync("FitNoteIT API is ok"));
app.RegisterUsersModuleRequests();

app.Run();