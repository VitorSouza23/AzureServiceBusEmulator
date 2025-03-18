# About

This is a simple project to demonstrate how to use Azure Service Bus Emulator with .NET Aspire.

## Environment
- .NET 9
- Aspire 9.1

## How to run
Go to the `AzureServiceBusEmulator.AppHost` folder and run the following command:
```bash
dotnet run
```
It will start the application and you will see the following message:
```bash
info: Aspire.Hosting.DistributedApplication[0]
      Login to the dashboard at http://localhost:15045/login?t=<random_token>
```

Open the URL in your browser and you will see the Aspire dashboard, wait for the application to be ready and then you can start sending messages to the Azure Service Bus Emulator.

To send a message, access the `WebApi` Swagger page at http://localhost:5104/swagger and use the `POST /api/messages` endpoint to send a message.

Back to the Aspire dashboard, you will see the messages being processed by the application on `BackgrounService` console logs.