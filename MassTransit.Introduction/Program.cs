using MassTransit;
using MassTransit.Introduction.Consumers;
using MassTransit.Introduction.Publishers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();
    
    busConfigurator.AddConsumer<CurrentTimeConsumer>();
    busConfigurator.AddConsumer<CurrentTimeConsumerV2>();

    busConfigurator.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        configurator.ConfigureEndpoints(context);
    });
});

builder.Services.AddHostedService<MessagePublisher>();

var app = builder.Build();

app.UseHttpsRedirection();
app.Run();