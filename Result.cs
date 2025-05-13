// ReSharper disable PropertyCanBeMadeInitOnly.Global
namespace Sefer.Backend.GeoIP.Api;

public class Result
{
    /// <summary>
    /// The unique id for this lookup
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the country where the IP address is located.
    /// </summary>
    public string? CountryName { get; set; }

    /// <summary>
    /// The ISO 3166-1 alpha-2 country code of the IP address location.
    /// </summary>
    public string? CountryCode { get; set; }

    /// <summary>
    /// The name of the city where the IP address is located.
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// The name of the region or state where the IP address is located.
    /// </summary>
    public string? Region { get; set; }

    /// <summary>
    /// The code of the region or state where the IP address is located.
    /// </summary>
    public string? RegionCode { get; set; }

    /// <summary>
    /// The name of the continent where the IP address is located.
    /// </summary>
    public string? Continent { get; set; }

    /// <summary>
    /// The latitude coordinate of the IP address location.
    /// </summary>
    public double? Latitude { get; set; }

    /// <summary>
    /// The longitude coordinate of the IP address location.
    /// </summary>
    public double? Longitude { get; set; }
}