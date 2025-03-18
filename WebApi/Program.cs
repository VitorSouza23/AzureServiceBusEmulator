using Azure.Messaging.ServiceBus;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddAzureServiceBusClient(connectionName: "messaging");

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/api/messages", async (ServiceBusClient client, string message) =>
{
    var sender = client.CreateSender("queue");
    await sender.SendMessageAsync(new ServiceBusMessage(message));
    return Results.Created("/api/messages", message);
});

app.Run();
