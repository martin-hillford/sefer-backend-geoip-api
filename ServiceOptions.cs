namespace Sefer.Backend.GeoIP.Api;

[SuppressMessage("ReSharper", "PropertyCanBeMadeInitOnly.Global")]
public class ServiceOptions
{
    public string? ApiKey { get; set; }

    public string? GeoDatabasePath { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public string? DatabaseConnectionString { get; set; }

    public string? MaxMindLicenseKey { get; set; }
}

public class ServiceOptionsProvider(IConfiguration configuration) : IServiceOptionsProvider
{
    private static ServiceOptions? _serviceOptions;

    public ServiceOptions GetServiceOptions()
    {
        _serviceOptions ??= new ServiceOptions
        {
            ApiKey = 
                EnvVar.GetEnvironmentVariable("GEOIP_API_KEY")  ??
                configuration.GetSection("GeoIP").GetValue<string>("ApiKey"),
            DatabaseConnectionString = 
                EnvVar.GetEnvironmentVariable("DATABASE_CONNECTION") ??
                configuration.GetSection("Database").GetValue<string>("ConnectionString"),
            GeoDatabasePath = 
                EnvVar.GetEnvironmentVariable("GEO_DB_PATH") ??
                configuration.GetSection("GeoIPService").GetValue<string>("GeoDatabasePath"),
            MaxMindLicenseKey = 
                EnvVar.GetEnvironmentVariable("MAX_MIND_LICENSE") ??
                configuration.GetSection("GeoIPService").GetValue<string>("MaxMindLicenseKey")
        };
        return _serviceOptions;
    }
}

public interface IServiceOptionsProvider
{
    public ServiceOptions GetServiceOptions();
}