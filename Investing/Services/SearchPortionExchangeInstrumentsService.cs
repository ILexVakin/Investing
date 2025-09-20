using Investing.Models;
using Investing.Models.ViewModels;
using Investing.Services.Interfaces;
using Investing.Services.MoexData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Investing.Services
{
    public class SearchPortionExchangeInstrumentsService
    {
        CurrencyData currencyData = new CurrencyData();
        StockData stockData = new StockData(); 
        List<SingleModelExchangeInstruments> listInstruments = new List<SingleModelExchangeInstruments>();
        public string SubstringInstrumentSearch {  get; set; }

        
        public async Task<List<SingleModelExchangeInstruments>> GetPartStockAsync()
        {
            var listStocks = await stockData.CombinedStockDataAsync();

            var foundStocks = listStocks.Where(p => p != null &&
                                                        p.Security != null &&
                                                       (p.Security.SHORTNAME?.Contains(SubstringInstrumentSearch, StringComparison.OrdinalIgnoreCase) == true ||
                                                        p.Security.SECNAME?.Contains(SubstringInstrumentSearch, StringComparison.OrdinalIgnoreCase) == true ||
                                                        p.Security.LATNAME?.Contains(SubstringInstrumentSearch, StringComparison.OrdinalIgnoreCase) == true))
                                                        .ToList();

            foreach (var foundStock in foundStocks) 
            {
                var stock = new SingleModelExchangeInstruments()
                {
                    ShortName = foundStock.Security.SHORTNAME,
                    PriceChange = foundStock.Marketdata.MARKETPRICE
                };
                listInstruments.Add(stock);
            }
            return listInstruments;
        }

        public async Task<List<SingleModelExchangeInstruments>> GetPartCurrencyAsync()
        {
          var listCurrency = await currencyData.CombinedCurrencyDataAsync();

          var foundCurrencyes = listCurrency.Where(p => p != null &&
                                                        p.Security != null &&
                                                        (p.Security.ShortName?.Contains(SubstringInstrumentSearch, StringComparison.OrdinalIgnoreCase) == true ||
                                                        p.Security.SecName?.Contains(SubstringInstrumentSearch, StringComparison.OrdinalIgnoreCase) == true ||
                                                        p.Security.SecId?.Contains(SubstringInstrumentSearch, StringComparison.OrdinalIgnoreCase) == true))
                                                        .ToList();

            foreach (var foundCurrency in foundCurrencyes)
            {
                var currency = new SingleModelExchangeInstruments()
                {
                    ShortName = foundCurrency.Security.ShortName,
                    PriceChange = foundCurrency.Marketdata.MarketPrice
                };
                listInstruments.Add(currency);
            }
            return listInstruments;
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
