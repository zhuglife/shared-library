using Microsoft.Extensions.Logging;

using StackExchange.Redis;

namespace Redis;
public class RedisConnectionProvider : IConnectionProvider
{
    private readonly IConnectionMultiplexer _connection;
    private readonly ILogger<RedisConnectionProvider> _logger;
    private bool _disposed;

    public bool IsConnected => _connection?.IsConnected ?? false;

    public RedisConnectionProvider(string connectionString, ILogger<RedisConnectionProvider> logger)
    {
        _logger = logger;

        var options = ConfigurationOptions.Parse(connectionString);
        options.AbortOnConnectFail = false;
        options.ConnectRetry = 3;
        options.ConnectTimeout = 5000;

        _connection = ConnectionMultiplexer.Connect(options);

        _connection.ConnectionFailed += OnConnectionFailed;
        _connection.ConnectionRestored += OnConnectionRestored;

        _logger.LogInformation("Redis connection established");
    }

    public IDatabase GetDatabase(int db = -1)
    {
        ThrowIfDisposed();
        return _connection.GetDatabase(db);
    }

    public IServer GetServer(string host, int port)
    {
        ThrowIfDisposed();
        return _connection.GetServer(host, port);
    }

    public ISubscriber GetSubscriber()
    {
        ThrowIfDisposed();
        return _connection.GetSubscriber();
    }

    public async Task<bool> TestConnectionAsync()
    {
        try
        {
            var db = GetDatabase();
            await db.PingAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Redis connection test failed");
            return false;
        }
    }

    private void OnConnectionFailed(object? sender, ConnectionFailedEventArgs e)
    {
        _logger.LogError(e.Exception, "Redis connection failed: {FailureType}", e.FailureType);
    }

    private void OnConnectionRestored(object? sender, ConnectionFailedEventArgs e)
    {
        _logger.LogInformation("Redis connection restored");
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
            throw new ObjectDisposedException(nameof(RedisConnectionProvider));
    }

    public void Dispose()
    {
        if (_disposed)
            return;

        _connection?.Dispose();
        _disposed = true;

        GC.SuppressFinalize(this);
    }
}
