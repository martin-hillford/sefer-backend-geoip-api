# Sefer.Backend.GeoIP.Api

This implementation serves as a lightweight wrapper for the GeoLite2 MaxMind City database. 
The API is designed to facilitate the secure download of the database while ensuring that no data is transmitted to 
third parties, thereby maintaining full compliance with GDPR regulations.

The web service necessitates the configuration of the following (environment) variables:

| Configuration Key              | Environment Var     | Description                                                           |
|--------------------------------|---------------------|-----------------------------------------------------------------------|
| GeoIP.ApiKey                   | GEOIP_API_KEY       | This API key will be used by your clients connecting with the service |
| GeoIPService.MaxmindLicenseKey | MAX_MIND_LICENSE    | The license key for downloading the GeoLite2 City database            |
| GeoIPService.GeoDatabasePath   | GEO_DB_PATH         | The location to store the GeoLite2 City database                      |
| Database.ConnectionString      | DATABASE_CONNECTION | Connection string to store lookups                                    |

Please note: Database connection is optional.
But it can be set for caching lookup for future analysis.
It is not required for internal caching, since a memory cache will be used for that.

Please be aware that the API utilizes PostgreSQL as its database for storage. At this time, no other databases are supported.