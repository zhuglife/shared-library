using StackExchange.Redis;

namespace Redis;

public interface IConnectionProvider : IDisposable
{
    IDatabase GetDatabase(int db = -1);
    IServer GetServer(string host, int port);
    ISubscriber GetSubscriber();
    bool IsConnected
    {
        get;
    }
    Task<bool> TestConnectionAsync();
}
