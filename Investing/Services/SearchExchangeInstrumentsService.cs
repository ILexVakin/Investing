using Investing.Data;
using Investing.Models;
using Investing.Services.Interfaces;
using Investing.Services.MoexData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Investing.Services
{
    public class SearchExchangeInstrumentsService : ISearchExchangeInstrumentsService
    {
        private readonly IFullModelInstrumentsMoex fullModelInstruments = new FullModelInstrumentsMoex();
        //IconCompany iconCompany = new IconCompany();
        SearchPortionExchangeInstrumentsService searchPortion = new SearchPortionExchangeInstrumentsService();
        public async Task<List<SingleModelExchangeInstruments>> SearchAllExchangeInstrumentsAsync()
        {
             var tasks = new List<Task<List<SingleModelExchangeInstruments>>>
             {
                fullModelInstruments.GetStockFullModelAsync(),
                fullModelInstruments.GetCurrencyFullModelAsync(),
                fullModelInstruments.GetBondFullModelAsync(),
                fullModelInstruments.GetFundFullModelAsync(),
                fullModelInstruments.GetFuturesFullModelAsync()
             };

            var resultAllTasks = await Task.WhenAll(tasks);
            return resultAllTasks.SelectMany(x => x).ToList();
        }

        public async Task<List<SingleModelExchangeInstruments>> SearchAllExchangeInstrumentsBySubstringAsync(string substring)
        {
            searchPortion.SubstringInstrumentSearch = substring;
            var tasks = new List<Task<List<SingleModelExchangeInstruments>>>
             {
               searchPortion.GetStocksBySubstringAsync(),
               searchPortion.GetCurrencyBySubstringAsync(),
               searchPortion.GetBondsBySubstringAsync(),
               searchPortion.GetFundsBySubstringAsync(),
               searchPortion.GetFuturesBySubstringAsync()
             };
            
            var resultAllTasks = await Task.WhenAll(tasks);
            return resultAllTasks.SelectMany(x => x).ToList();
        }

        //будет один метод, содержащий в себе как минимум 2 других. 
        //1) Получить изображения из redis
        //2) Получить данные которые дублируются (оригинал и его дубль isin) Dictionary<string, string> iconWhichRepeated = new Dictionary<string, string>();
    }
    public class IconCompany
    {
        string[] isin = new string[] { "dsfs" };
        private readonly MainContext _context;
        public IconCompany(MainContext context)
        {
            _context = context;
        }
        public async Task<SingleModelExchangeInstruments> CombineInstrumentsWithIcon(List<SingleModelExchangeInstruments> listInstruments)
        {
            return new SingleModelExchangeInstruments();
        }

        public async Task<Dictionary<string, byte[]>> GetAllImage()
        {
            Dictionary<string, byte[]> keyValuePairs = new Dictionary<string, byte[]>();
            var tasks = new List<Task<Dictionary<string, byte[]>>>
            {
                GetIconFromRedis(isin),
                GetIconFromPg(isin)
            };

            var resultAllTasks = await Task.WhenAll(tasks);

            return (Dictionary<string, byte[]>)resultAllTasks.SelectMany(x => x);
        }
        public async Task<Dictionary<string, byte[]>> GetIconFromRedis(string[] isin)
        {
            Dictionary<string, byte[]> keyValuePairs = new Dictionary<string, byte[]>();
            return keyValuePairs;
        }
        public async Task<Dictionary<string, byte[]>> GetIconFromPg(string[] isin)
        {
            Dictionary<string, byte[]> keyValuePairs = new Dictionary<string, byte[]>();
            var allDuplicateIcon =  _context.DuplicateRedis.ToList();
            return keyValuePairs;
        }

    }
}
