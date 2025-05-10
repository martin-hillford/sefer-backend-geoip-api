namespace Sefer.Backend.GeoIP.Api;

public static class Storage
{
    private const string Query = "INSERT INTO ip_address_lookups(id,ip_address,date,country_name,country_code,city,region,region_code,continent,latitude,longitude) VALUES(@id, @ipAddress, @date, @countryName, @countryCode, @city, @region, @regionCode, @continent, @latitude, @longitude )";

    private static readonly string? ConnectionString = Environment.GetEnvironmentVariable("STORAGE_DATABASE");

    public static async Task<bool> SaveResultAsync(string ipAddress, Result data)
    {
        try
        {
            if (string.IsNullOrEmpty(ConnectionString)) return false;

            await using var connection = new NpgsqlConnection(ConnectionString);
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