using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investing.Redis.Data
{
    internal class Context
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
