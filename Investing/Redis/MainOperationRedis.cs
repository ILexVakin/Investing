using Investing.Redis.Services;
using System.Threading.Tasks;
using System;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;

namespace Investing.Redis
{
    public class MainOperationRedis : IMainOperationRedis
    {
        private readonly IDatabase _db;
        public MainOperationRedis(IDatabase db)
        {
            _db = db;
        }

        async Task IMainOperationRedis.AddElementRedis(string key, string value, TimeSpan? expiry)
        {
            var element = TransformationData.Serialize(value);
            await _db.StringSetAsync(key, element, expiry);
        }

        async Task<byte[]> IMainOperationRedis.GetElementRedis(string key)
        {
            var element = await _db.StringGetAsync(key);
            if (element.IsNullOrEmpty)
            {
                return null;
            }
            return TransformationData.Deserialize<byte[]>(element);
        }

    async Task<bool> IMainOperationRedis.RemoveElementKeyRedis(string key)
        {
            return await _db.KeyDeleteAsync(key);
        }
    }
}
