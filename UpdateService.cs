namespace Sefer.Backend.GeoIP.Api;

public class UpdateService(IServiceOptionsProvider serviceOptionsProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var options = serviceOptionsProvider.GetServiceOptions();
        var period = TimeSpan.FromDays(1);
        using var timer = new PeriodicTimer(period);
        while (await timer.WaitAsync(stoppingToken))
        {
            try
            {
                await Updater.Update(options);
            }
            catch (Exception) { }
        }
    }
}