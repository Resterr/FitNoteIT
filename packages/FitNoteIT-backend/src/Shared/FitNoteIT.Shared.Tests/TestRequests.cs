using Microsoft.Extensions.DependencyInjection;

namespace FitNoteIT.Shared.Tests;
public abstract class TestRequests
{
    protected HttpClient Client { get; }

    public TestRequests()
    {
        var app = new TestApp(ConfigureServices);
        Client = app.Client;
    }
    
    protected virtual void ConfigureServices(IServiceCollection services)
    {
    }
}