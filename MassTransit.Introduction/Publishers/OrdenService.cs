using MassTransit.Introduction.Models;

namespace MassTransit.Introduction.Publishers
{
    public class OrdenService(ISendEndpointProvider sendEndpointProvider)
    {
        private readonly ISendEndpointProvider _sendEndpointProvider = sendEndpointProvider;

        public async Task CrearOrden()
        {
            var endpoint = await _sendEndpointProvider
                .GetSendEndpoint(new Uri("queue:crear-orden-queue"));

            await endpoint.Send(new CrearOrdenMessage
            {
                OrdenId = Guid.NewGuid(),
                Cliente = "Juan Perez",
                Monto = 1000
            });
        }
    }
}
