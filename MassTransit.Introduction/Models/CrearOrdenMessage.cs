namespace MassTransit.Introduction.Models
{
    public record CrearOrdenMessage
    {
        public Guid OrdenId { get; init; }
        public string Cliente { get; init; } = string.Empty;
        public decimal Monto { get; init; }
    }
}
