using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Investing.Redis
{
    public interface IMainOperationRedis
    {
        Task<byte[]> GetElementRedis(string key);
        Task AddElementRedis(string key, string value, TimeSpan? expiry);
        Task<bool> RemoveElementKeyRedis(string key);
    }
}
