namespace MassTransit.Introduction.Consumers;

public record CurrentTime
{
    public string Value { get; init; } = string.Empty;
}