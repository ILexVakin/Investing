using StackExchange.Redis;

namespace Investing.Redis
{
    public class ContextRedis
    {
        public IDatabase ConnectionRedis()
        {
            IDatabase _db;
            string connectionString = "localhost:6379";
            using (IConnectionMultiplexer redis = ConnectionMultiplexer.Connect(connectionString))
            {
                _db = redis.GetDatabase();
            }

            return _db;
        }
    }
}
