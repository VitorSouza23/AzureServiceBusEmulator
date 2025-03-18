using Azure.Messaging.ServiceBus;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddAzureServiceBusClient(connectionName: "messaging");
builder.Services.AddLogging();
builder.Services.AddHostedService<QueueBackgroundService>();

var app = builder.Build();


app.Run();

class QueueBackgroundService(ILogger<QueueBackgroundService> logger, ServiceBusClient client) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var receiver = client.CreateReceiver("queue");
        return Task.Run(async () =>
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var message = await receiver.ReceiveMessageAsync();
                if (message != null)
                {
                    logger.LogInformation("Received message: {MessageBody}", message.Body.ToString());
                    await receiver.CompleteMessageAsync(message);
                }
            }
        }, stoppingToken);
    }
}