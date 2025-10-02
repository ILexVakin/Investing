using StackExchange.Redis;

namespace Investing.Redis.Services
{
    internal class IconCompany : IMainOperationRedis<string>
    {
        private readonly IDatabase _db;
        public IconCompany(IDatabase db)
        {
            _db = db;
        }

        async Task IMainOperationRedis<string>.AddElementRedis(string key, string value, TimeSpan? expiry)
        {
            var icon = TransformationData.Serialize(value);
            await _db.StringSetAsync(key, icon, expiry);
        }

        async Task<string> IMainOperationRedis<string>.GetElementRedis(string key)
        {
            var icon =  await _db.StringGetAsync(key);
            if (icon.IsNullOrEmpty)
            {
                return string.Empty;
            }
            return TransformationData.Deserialize<string>(icon);
        }

        async Task<bool> IMainOperationRedis<string>.RemoveElementKeyRedis(string key)
        {
            return await _db.KeyDeleteAsync(key);
        }
    }
}
