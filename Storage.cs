namespace Sefer.Backend.GeoIP.Api;

public class Storage(ServiceOptions options)
{
    private const string Query = "INSERT INTO ip_address_lookups(id,ip_address,date,country_name,country_code,city,region,region_code,continent,latitude,longitude) VALUES(@id, @ipAddress, @date, @countryName, @countryCode, @city, @region, @regionCode, @continent, @latitude, @longitude )";

    public static Storage Create(ServiceOptions options) => new(options);
    
    public async Task<bool> SaveResultAsync(string ipAddress, Result data)
    {
        try
        {
            if (string.IsNullOrEmpty(options.DatabaseConnectionString)) return false;

            await using var connection = new NpgsqlConnection(options.DatabaseConnectionString);
            await using var command = new NpgsqlCommand(Query, connection);

            var parameters = new NpgsqlParameter[]
            {
                new("@id", data.Id),
                new("@ipAddress", ipAddress),
                new("@date", System.Data.SqlDbType.DateTime2) { Value = DateTime.UtcNow },
                new("@countryName", data.CountryName),
                new("@countryCode", data.CountryCode),
                new("@city", data.City),
                new("@region", data.Region),
                new("@regionCode", data.RegionCode),
                new("@continent", data.Continent),
                new("@latitude", data.Latitude),
                new("@longitude", data.Longitude),
            };

            command.Parameters.AddRange(parameters);
            connection.Open();
            var result = await command.ExecuteNonQueryAsync();
            return result == 1;
        }
        catch (Exception) { return false; }
    }
}