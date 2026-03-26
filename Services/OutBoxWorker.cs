using System;

namespace apiBukLitoprocess.Services;

public class OutBoxWorker : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
          while (!stoppingToken.IsCancellationRequested)
        {
            
            Console.WriteLine($"OutBoxWorker is running at: {DateTimeOffset.Now}");
            

            // Simulate some work with a delay
            await Task.Delay(1000, stoppingToken);
        }

    }
}
