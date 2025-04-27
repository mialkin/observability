# Observability

This solution showcases the observability setup within the context of .NET.

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