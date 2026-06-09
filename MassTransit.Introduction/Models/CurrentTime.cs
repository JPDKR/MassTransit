namespace MassTransit.Introduction.Models;

public record CurrentTime
{
    public string Value { get; init; } = string.Empty;
    public int PropiedadNumericaFantastica { get; set; }
}