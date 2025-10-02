using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investing.Redis.Services
{
    internal interface IMainOperationRedis<T>
    {
        Task<T> GetElementRedis(string key);
        Task AddElementRedis(string key, string value, TimeSpan? expiry);
        Task<bool> RemoveElementKeyRedis(string key);
    }
}
