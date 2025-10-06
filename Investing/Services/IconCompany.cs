using Investing.Data;
using Investing.Models;
using Investing.Redis;
using Investing.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Investing.Services
{
    public class IconCompany : IIconCompany
    { 
       private readonly IMainOperationRedis _mainOperationRedis;

        public IconCompany()
        {
            ContextRedis contRedis = new ContextRedis();
            var t = contRedis.ConnectionRedis();
            _mainOperationRedis = new MainOperationRedis(t);
        }

        public async Task<byte[]> GetIconCompany(string isin)
        {  
            //если не нашли в редис, тогдаделаем обращение к pg
            var tasks = new List<Task<Dictionary<string, byte[]>>>
            {
                GetIconFromRedis(isin),
                GetIconFromPg(isin)
            };

            var resultAllTasks = await Task.WhenAll(tasks);
            return resultAllTasks.Select(g => g.FirstOrDefault().Value).First();
        }
        public async Task<Dictionary<string, byte[]>> GetIconFromRedis(string isin)
        {
            Dictionary<string, byte[]> keyValuePairs = new Dictionary<string, byte[]>();
            var valueIcon =  await _mainOperationRedis.GetElementRedis(isin);
            keyValuePairs.Add(isin, valueIcon);
            return keyValuePairs;
        }
        public async Task<Dictionary<string, byte[]>> GetIconFromPg(string isin)
        {
            Dictionary<string, byte[]> keyValuePairs = new Dictionary<string, byte[]>();
            //var allDuplicateIcon = _context.DuplicateRedis.ToList();
            return keyValuePairs;
        }
    }
}
