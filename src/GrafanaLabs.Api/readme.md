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

Prometheus UI: <http://localhost:6300/targets>.

Grafana UI: <http://localhost:6100/datasources>.

Alloy UI: <http://localhost:6500/>.

## Produce metrics

Visit <http://localhost:5130/counter/increment?value=4>, then search for `simple_meter_total` metrics inside
Prometheus.

Datasource URLs:

- Prometheus: <http://host.docker.internal:6300>
- Loki: <http://host.docker.internal:6200>
