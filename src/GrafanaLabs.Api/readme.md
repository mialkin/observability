# Grafana Labs stack

## Prerequisites

- [↑ .NET SDK 9](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [↑ Docker Desktop](https://www.docker.com/products/docker-desktop/)

## Run application

Run infrastructure:

```bash
docker compose --file infrastructure.yaml up --detach
```

Run application:

```bash
dotnet run
```

Shut down infrastructure:

```bash
docker compose --file infrastructure.yaml down
```

## Prometheus

Prometheus UI: <http://localhost:5140/targets>.

Grafana UI: <http://localhost:5150/datasources>. Use <http://host.docker.internal:5140> as Prometheus' datasource URL.

Visit the link <http://localhost:5130/counter/increment?value=4> then search for `simple_meter_total` metrics inside
Prometheus.
