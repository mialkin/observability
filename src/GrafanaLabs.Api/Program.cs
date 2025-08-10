var builder = WebApplication.CreateBuilder(args);

var application = builder.Build();

application.MapGet("/", () => "GrafanaLabs.Api");

application.Run();
