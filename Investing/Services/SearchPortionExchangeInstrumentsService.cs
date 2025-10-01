using Investing.Models;
using Investing.Models.ViewModels;
using Investing.Services.Interfaces;
using Investing.Services.MoexData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Investing.Services
{
    public class SearchPortionExchangeInstrumentsService
    {
        private readonly IFullModelInstrumentsMoex fullModelInstruments = new FullModelInstrumentsMoex();
        public string SubstringInstrumentSearch {  get; set; }
        
        public async Task<List<SingleModelExchangeInstruments>> GetStocksBySubstringAsync()
        {
            var listStocks = await fullModelInstruments.GetStockFullModelAsync();
            var t =  listStocks.Where(c => c != null && (c.ShortName?.Contains(SubstringInstrumentSearch, StringComparison.OrdinalIgnoreCase) == true)).ToList();
            foreach (var foundStock in t)
            {
                Debug.WriteLine(foundStock.ShortName);
            }
            return listStocks.Where(c => c != null && (c.ShortName?.Contains(SubstringInstrumentSearch, StringComparison.OrdinalIgnoreCase) == true)).ToList();
        }

       
        //public async Task<SingleModelExchangeInstruments> GetPartBondsAsync(string query)
        //{

        //} 
        //public async Task<SingleModelExchangeInstruments> GetPartFundsAsync(string query)
        //{

        //} 
        //public async Task<SingleModelExchangeInstruments> GetPartFuturesAsync(string query)
        //{

        //}
    }
}
