using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Data.Common;

namespace HealthChecks.Common.Checks;

public class DatabaseHealthCheck : IHealthCheck
{
    private readonly DbConnection _connection;

    public DatabaseHealthCheck(DbConnection connection)
    {
        _connection = connection;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await _connection.OpenAsync(cancellationToken);
            
            var command = _connection.CreateCommand();
            command.CommandText = "SELECT 1";
            await command.ExecuteScalarAsync(cancellationToken);
            
            await _connection.CloseAsync();

            return HealthCheckResult.Healthy("Database is responsive");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(
                "Database is not responsive",
                ex);
        }
    }
}
