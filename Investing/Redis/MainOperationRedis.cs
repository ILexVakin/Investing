using Investing.Redis.Services;
using System.Threading.Tasks;
using System;
using StackExchange.Redis;

namespace Investing.Redis
{
    public class MainOperationRedis : IMainOperationRedis<string>
    {
        private readonly IDatabase _db;
        public MainOperationRedis(IDatabase db)
        {
            _db = db;
        }

        async Task IMainOperationRedis<string>.AddElementRedis(string key, string value, TimeSpan? expiry)
        {
            var element = TransformationData.Serialize(value);
            await _db.StringSetAsync(key, element, expiry);
        }

        async Task<string> IMainOperationRedis<string>.GetElementRedis(string key)
        {
            var element = await _db.StringGetAsync(key);
            if (element.IsNullOrEmpty)
            {
                return string.Empty;
            }
            return TransformationData.Deserialize<string>(element);
        }

        async Task<bool> IMainOperationRedis<string>.RemoveElementKeyRedis(string key)
        {
            return await _db.KeyDeleteAsync(key);
        }
    }
}
