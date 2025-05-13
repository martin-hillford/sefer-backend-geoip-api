namespace Sefer.Backend.GeoIP.Api;

public static class Updater
{
    public static async Task Update(ServiceOptions options)
    {
        try
        {
            // Check if the folder for the geo database from maxmind exists
            var directory = Path.GetDirectoryName(options.GeoDatabasePath);
            if (directory != null && !Directory.Exists(directory)) Directory.CreateDirectory(directory);

            // Download and extract the city database
            var uri = $"https://download.maxmind.com/app/geoip_download?edition_id=GeoLite2-City&license_key={options.MaxMindLicenseKey}&suffix=tar.gz";
            var client = new HttpClient();
            await using var downloadStream = await client.GetStreamAsync(uri);
            await using var gzipStream = new GZipStream(downloadStream, CompressionMode.Decompress);
            var tempDirectory = GetTemporaryDirectory();
            await TarFile.ExtractToDirectoryAsync(gzipStream, tempDirectory, true);

            // The tar file contains a data specific folder
            var database = Directory.GetFiles(tempDirectory, "*.mmdb", SearchOption.AllDirectories).First();
            File.Copy(database, options.GeoDatabasePath!, true);
            Directory.Delete(tempDirectory, true);
        }
        catch (Exception) { }
    }

    private static string GetTemporaryDirectory()
    {
        string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        Directory.CreateDirectory(tempDirectory);
        return tempDirectory;
    }
}