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

        SearchPortionExchangeInstrumentsService searchPortion = new SearchPortionExchangeInstrumentsService();
        public async Task<List<SingleModelExchangeInstruments>> SearchAllExchangeInstrumentsAsync()
        {
             var tasks = new List<Task<List<SingleModelExchangeInstruments>>>
             {
                fullModelInstruments.GetStockFullModelAsync(),
                fullModelInstruments.GetCurrencyFullModelAsync(),
                fullModelInstruments.GetBondFullModelAsync()
             };

            var resultAllTasks = await Task.WhenAll(tasks);
            return resultAllTasks.SelectMany(x => x).ToList();
        }

        public async Task<List<SingleModelExchangeInstruments>> SearchAllExchangeInstrumentsBySubstringAsync(string substring)
        {
            searchPortion.SubstringInstrumentSearch = substring;
            var tasks = new List<Task<List<SingleModelExchangeInstruments>>>
             {
               searchPortion.GetStocksBySubstringAsync()
             };

            var resultAllTasks = await Task.WhenAll(tasks);
            return resultAllTasks.SelectMany(x => x).ToList();
        }
    }
}
