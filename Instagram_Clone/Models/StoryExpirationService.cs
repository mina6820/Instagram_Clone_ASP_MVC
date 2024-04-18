using System;
using System.Threading;
using System.Threading.Tasks;
using Instagram_Clone;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class StoryExpirationService : BackgroundService
{
    private readonly IServiceProvider _services;
    private readonly ILogger<StoryExpirationService> _logger;

    public StoryExpirationService(IServiceProvider services, ILogger<StoryExpirationService> logger)
    {
        _services = services;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<Context>();

                // Retrieve stories from the database
                var stories = dbContext.Stories;

                // Check expiration for each story
                foreach (var story in stories)
                {
                    story.CheckExpiration();
                }

                // Save changes to the database
                await dbContext.SaveChangesAsync(stoppingToken);
            }

            // Wait for a specific interval before checking again
            await Task.Delay(TimeSpan.FromHours(1), stoppingToken); // Adjust the delay as needed
        }
    }
}
