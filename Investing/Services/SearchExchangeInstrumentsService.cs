using Investing.Models;
using Investing.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Investing.Services
{
    public class SearchExchangeInstrumentsService : ISearchExchangeInstrumentsService
    {
        SearchPortionExchangeInstrumentsService searchPortion = new SearchPortionExchangeInstrumentsService();

        public async Task<List<SingleModelExchangeInstruments>> SearchAllExchangeInstrumentsAsync(string substringInstrumentSearch)
        {
             searchPortion.SubstringInstrumentSearch = substringInstrumentSearch;
             var tasks = new List<Task<List<SingleModelExchangeInstruments>>>
             {
                searchPortion.GetPartStockAsync(),
                searchPortion.GetPartCurrencyAsync()
             };

            var resultAllTasks = await Task.WhenAll(tasks);
            return resultAllTasks.SelectMany(x => x).ToList();
        }
    }
}
