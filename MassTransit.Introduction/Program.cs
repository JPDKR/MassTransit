using MassTransit;
using MassTransit.Introduction.Consumers;
using MassTransit.Introduction.Publishers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddMassTransit(busConfigurator =>
{
    // Configurar DB - MassTransit.EntityFrameworkCore
    //busConfigurator.AddEntityFrameworkOutbox<ApplicationDbContext>(o =>
    //{
    //    o.UseSqlServer();
    //    o.UseBusOutbox();
    //});

    busConfigurator.SetKebabCaseEndpointNameFormatter();

    busConfigurator.AddConsumer<CrearOrdenConsumer>();

    busConfigurator.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        configurator.ReceiveEndpoint("crear-orden-queue", e =>
        {
            e.ConcurrentMessageLimit = 10;
            e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
            e.ConfigureConsumer<CrearOrdenConsumer>(context);
        });
    });
});

builder.Services.AddHostedService<MessagePublisher>();

var app = builder.Build();

app.UseHttpsRedirection();
app.Run();