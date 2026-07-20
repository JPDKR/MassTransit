using MassTransit;
using MassTransit.Introduction.Configuration;
using MassTransit.Introduction.Consumers;
using MassTransit.Introduction.Publishers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

var rabbitMqSettings = builder.Configuration.GetSection(RabbitMqSettings.SectionName).Get<RabbitMqSettings>()
    ?? new RabbitMqSettings();

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
        configurator.Host(rabbitMqSettings.Host, rabbitMqSettings.VirtualHost, h =>
        {
            h.Username(rabbitMqSettings.Username);
            h.Password(rabbitMqSettings.Password);
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