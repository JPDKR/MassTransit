using MassTransit;
using MassTransit.Introduction.Consumers;

namespace MassTransit.Introduction.Publishers
{
    public class MessagePublisher(IBus bus) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await bus.Publish
                    (new CurrentTime
                    {
                        Value = $"El tiempo es {DateTime.UtcNow}"
                    },
                    stoppingToken);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}