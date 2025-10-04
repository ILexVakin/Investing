using System.Threading.Tasks;
using System;

namespace Investing.Redis
{
    public interface IMainOperationRedis<T>
    {
        Task<T> GetElementRedis(string key);
        Task AddElementRedis(string key, string value, TimeSpan? expiry);
        Task<bool> RemoveElementKeyRedis(string key);
    }
}
