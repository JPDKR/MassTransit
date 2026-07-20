# MassTransit + RabbitMQ - Introducción

Proyecto de ejemplo que muestra el uso de [MassTransit](https://masstransit.io/) sobre RabbitMQ en una app .NET (Worker/Web Host), con publicación periódica de mensajes y un consumer con reintentos y límite de concurrencia.

## Requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- RabbitMQ corriendo localmente (host `localhost`, usuario/contraseña `guest`/`guest`)

### Levantar RabbitMQ con Docker

```bash
docker run -d --name rabbitmq \
  -p 5672:5672 -p 15672:15672 \
  rabbitmq:3-management
```

La consola de administración queda disponible en http://localhost:15672 (usuario `guest`, contraseña `guest`).

## Cómo correr el proyecto

```bash
cd MassTransit.Introduction
dotnet run
```

Al arrancar, `MessagePublisher` (un `BackgroundService`) publica un `CrearOrdenMessage` cada segundo, y `CrearOrdenConsumer` los va consumiendo desde la cola `crear-orden-queue`.

## Estructura

```
MassTransit.Introduction/
├── Consumers/
│   └── CrearOrdenConsumer.cs     # Consume CrearOrdenMessage
├── Models/
│   └── CrearOrdenMessage.cs      # Contrato del mensaje (OrdenId, Cliente, Monto)
├── Publishers/
│   └── MessagePublisher.cs       # BackgroundService que publica mensajes periódicamente
└── Program.cs                    # Configuración de MassTransit y RabbitMQ
```

## Configuración de MassTransit

En `Program.cs`:

- Se registra `CrearOrdenConsumer` y se conecta contra RabbitMQ en `localhost`.
- El consumer se bindea explícitamente a la cola `crear-orden-queue`, con:
  - `ConcurrentMessageLimit = 10` (hasta 10 mensajes en paralelo)
  - Reintentos: 3 intentos con 5 segundos de intervalo (`UseMessageRetry`)

## Notas

- Las credenciales de RabbitMQ están hardcodeadas (`guest`/`guest`) para simplificar el ejemplo; en un proyecto real deberían salir de configuración/secrets.
- Proyecto pensado como introducción práctica a MassTransit, no como base de producción.
