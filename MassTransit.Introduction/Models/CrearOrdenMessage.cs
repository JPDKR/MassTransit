namespace MassTransit.Introduction.Models
{
    public record CrearOrdenMessage
    {
        public Guid OrdenId { get; init; }
        public string Cliente { get; init; }
        public decimal Monto { get; init; }
    }
}
