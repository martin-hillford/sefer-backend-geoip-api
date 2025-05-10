namespace Sefer.Backend.GeoIP.Api;

public class ServiceOptions
{
    public string? ApiKey { get; set; } = default;

    public string? GeoDatabasePath { get; set; } = default;

    public string? DatabaseConnectionString { get; set; } = default;

    public string? MaxMindLicenseKey { get; set; } = default;
}

public class ServiceOptionsProvider(IConfiguration configuration) : IServiceOptionsProvider
{
    private static ServiceOptions? _serviceOptions;

    public ServiceOptions GetServiceOptions()
    {
        _serviceOptions ??= new()
        {
            ApiKey = configuration.GetSection("GeoIP").GetValue<string>("ApiKey"),
            DatabaseConnectionString = configuration.GetSection("Database").GetValue<string>("ConnectionString"),
            GeoDatabasePath = configuration.GetSection("GeoIPService").GetValue<string>("GeoDatabasePath"),
            MaxMindLicenseKey = configuration.GetSection("GeoIPService").GetValue<string>("MaxMindLicenseKey")
        };
        return _serviceOptions;
    }
}

public interface IServiceOptionsProvider
{
    public ServiceOptions GetServiceOptions();
}