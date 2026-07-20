using MassTransit.Introduction.Models;

namespace MassTransit.Introduction.Consumers
{
    public class CrearOrdenConsumer(ILogger<CrearOrdenConsumer> logger) : IConsumer<CrearOrdenMessage>
    {
        private readonly ILogger<CrearOrdenConsumer> _logger = logger;

        public async Task Consume(ConsumeContext<CrearOrdenMessage> context)
        {
            var mensaje = context.Message;

            _logger.LogInformation(
                "Procesando orden {OrdenId} para {Cliente}. Monto ${Monto}",
                mensaje.OrdenId,
                mensaje.Cliente,
                mensaje.Monto);

            // Simulación de trabajo
            await Task.Delay(2000);

            _logger.LogInformation(
                "Orden {OrdenId} procesada",
                mensaje.OrdenId);
        }
    }
}
