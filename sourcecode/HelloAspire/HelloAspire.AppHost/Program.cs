var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.HelloAspire_ApiService>("apiservice");

builder.AddProject<Projects.HelloAspire_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
