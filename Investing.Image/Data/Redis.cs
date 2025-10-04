using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investing.Image.Data
{
    internal class Redis
    {
        public async Task InsertIconsInRedis(Dictionary<string, byte[]> redisIcons)
        {
            StackExchange.Redis.IDatabase _db;
            using (IConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379"))
            {
                _db = redis.GetDatabase();

                foreach (var item in redisIcons)
                {
                    var icon = JsonConvert.SerializeObject(item.Value);
                    await _db.StringSetAsync(item.Key, icon, TimeSpan.FromSeconds(86400));
                }
            }
        }
    }
}
