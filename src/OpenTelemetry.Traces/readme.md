# OpenTelemetry.Traces

This project showcases the OpenTelemetry setup
for [↑ tracing](https://opentelemetry.io/docs/languages/dotnet/instrumentation).

## Prerequisites

- [↑ .NET SDK 9](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [↑ Docker Desktop](https://www.docker.com/products/docker-desktop/)

## Run infrastructure

```bash
docker compose --file infrastructure.yaml up --detach
```

Jaeger UI: <http://localhost:16686/search>.

## Run application

```bash
dotnet run
```

## Shut down infrastructure

```bash
docker compose --file infrastructure.yaml down
```