using Confluent.Kafka;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthChecks.Common.Checks;

public class KafkaHealthCheck : IHealthCheck
{
    private readonly string _bootstrapServers;

    public KafkaHealthCheck(string bootstrapServers)
    {
        _bootstrapServers = bootstrapServers;
    }

    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var config = new AdminClientConfig
            {
                BootstrapServers = _bootstrapServers,
                SocketTimeoutMs = 5000
            };

            using var adminClient = new AdminClientBuilder(config).Build();
            var metadata = adminClient.GetMetadata(TimeSpan.FromSeconds(5));

            if (metadata.Brokers.Count > 0)
            {
                return Task.FromResult(HealthCheckResult.Healthy(
                    $"Kafka is healthy. Brokers: {metadata.Brokers.Count}"));
            }

            return Task.FromResult(HealthCheckResult.Degraded("No Kafka brokers available"));
        }
        catch (Exception ex)
        {
            return Task.FromResult(HealthCheckResult.Unhealthy("Kafka is not available", ex));
        }
    }
}
