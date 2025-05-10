namespace Sefer.Backend.GeoIP.Api;

public static class Setup
{
    public static WebApplication CreateApp(string[] args)
    {
        // Now create the app
        var builder = WebApplication.CreateBuilder(args);

        builder.WithSharedConfig();
        builder.Services.AddHostedService<UpdateService>();
        builder.Services.AddMemoryCache();
        builder.Services.AddSingleton<IServiceOptionsProvider, ServiceOptionsProvider>();

        return builder.Build();
    }
}