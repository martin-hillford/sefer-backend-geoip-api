namespace Sefer.Backend.GeoIP.Api;

public static class Extensions
{
    public static async Task<bool> WaitAsync(this PeriodicTimer timer, CancellationToken stoppingToken)
    {
        return !stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken);
    }
}