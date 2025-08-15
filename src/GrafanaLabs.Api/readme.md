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

## UI

Prometheus UI: <http://localhost:5140/targets>.

Grafana UI: <http://localhost:5150/datasources>. Datasource URLs:

- Prometheus: <http://host.docker.internal:5140>
- Loki: <http://host.docker.internal:5160>

Alloy UI: <http://localhost:5170/>.

## Produce metrics

Visit the link <http://localhost:5130/counter/increment?value=4> then search for `simple_meter_total` metrics inside
Prometheus.
