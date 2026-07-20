using MassTransit.Introduction.Models;

namespace MassTransit.Introduction.Publishers
{
    public class MessagePublisher(IBus bus) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await bus.Publish
                    (new CrearOrdenMessage
                    {
                        OrdenId= Guid.NewGuid(),
                        Cliente = "El Pepe",
                        Monto = Random.Shared.Next(),

                    },
                    stoppingToken);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}