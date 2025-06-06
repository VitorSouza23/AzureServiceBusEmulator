var builder = DistributedApplication.CreateBuilder(args);

var serviceBus = builder.AddAzureServiceBus("messaging")
                        .RunAsEmulator();
serviceBus.AddServiceBusQueue("queue");

builder.AddProject<Projects.WebApi>("webapi")
                    .WithUrlForEndpoint("http", url =>
                    {
                        url.DisplayText = "Swagger UI";
                    })
                    .WithReference(serviceBus)
                    .WaitFor(serviceBus);
builder.AddProject<Projects.BackgroundService>("background")
                    .WithReference(serviceBus)
                    .WaitFor(serviceBus);

builder.Build().Run();
