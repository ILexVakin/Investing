using Newtonsoft.Json;
using StackExchange.Redis;

namespace Investing.Redis.Services
{
    public static class TransformationData
    {
        public static string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T Deserialize<T>(RedisValue json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
