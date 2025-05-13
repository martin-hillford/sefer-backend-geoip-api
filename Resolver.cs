namespace Sefer.Backend.GeoIP.Api;

public class Resolver(ServiceOptions options)
{
    private static DatabaseReader? _reader;

    public async Task<Result?> Lookup(string ipAddress)
    {
        // Check if the database with the geo information is available
        var fileLocation = options.GeoDatabasePath!;
        if (!File.Exists(fileLocation)) await Updater.Update(options);

        // Ensure a reader is available
        _reader ??= new DatabaseReader(fileLocation);

        // And try to resolve the ip
        try
        {
            // Get the ip information and do an additional check to ensure a response is give
            var result = _reader.City(ipAddress);
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (result == null) return null;

            return new Result
            {
                Id = Guid.NewGuid(),
                Continent = result.Continent.Name,
                CountryCode = result.Country.IsoCode,
                CountryName = result.Country.Name,
                City = result.City.Name,
                Latitude = result.Location.Latitude,
                Longitude = result.Location.Longitude,
                Region = result.MostSpecificSubdivision.Name,
                RegionCode = result.MostSpecificSubdivision.IsoCode
            };
        }
        catch (Exception) { return null; }
    }
}