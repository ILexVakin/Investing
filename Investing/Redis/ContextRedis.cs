using StackExchange.Redis;

namespace Investing.Redis
{
    public class ContextRedis
    {
        private static IConnectionMultiplexer _redis;
        private static readonly object locks = new object();

        public IDatabase ConnectionRedis()
        {
            if (_redis == null || !_redis.IsConnected)
            {
                lock (locks)
                {
                    if (_redis == null || !_redis.IsConnected)
                    {
                        var connectionString = "localhost:6379,abortConnect=false,connectTimeout=30000";
                        _redis = ConnectionMultiplexer.Connect(connectionString);
                    }
                }
            }
            return _redis.GetDatabase();
        }
    }
}
