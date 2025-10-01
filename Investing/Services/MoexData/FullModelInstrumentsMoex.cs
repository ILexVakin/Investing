using Investing.Models;
using Investing.Services.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
using System.Threading.Tasks;

namespace Investing.Services.MoexData
{
    public class FullModelInstrumentsMoex : IFullModelInstrumentsMoex
    {
        CurrencyData currencyData = new CurrencyData();
        StockData stockData = new StockData();
        BondData bondData = new BondData();
        public async Task<List<SingleModelExchangeInstruments>> GetStockFullModelAsync()
        {
            List<SingleModelExchangeInstruments> listInstruments = new List<SingleModelExchangeInstruments>();
            var listStocks = await stockData.CombinedStockDataAsync();
            foreach (var foundStock in listStocks)
            {
                var stock = new SingleModelExchangeInstruments()
                {
                    ShortName = foundStock.Security.SHORTNAME,
                    PriceChange = foundStock.Marketdata.MARKETPRICE,
                    TypeInstrument = TypeInstrument.Stock
                };
                listInstruments.Add(stock);
            }
            return listInstruments;
        }
        public async Task<List<SingleModelExchangeInstruments>> GetCurrencyFullModelAsync()
        {
            List<SingleModelExchangeInstruments> listInstruments = new List<SingleModelExchangeInstruments>();
            var listCurrency = await currencyData.CombinedCurrencyDataAsync();

            foreach (var foundCurrency in listCurrency)
            {
                var currency = new SingleModelExchangeInstruments()
                {
                    ShortName = foundCurrency.Security.ShortName,
                    PriceChange = foundCurrency.Marketdata.MarketPrice,
                    TypeInstrument = TypeInstrument.Currency
                };
                listInstruments.Add(currency);
            }
            return listInstruments;
        }
        public async Task<List<SingleModelExchangeInstruments>> GetBondFullModelAsync()
        {
            List<SingleModelExchangeInstruments> listInstruments = new List<SingleModelExchangeInstruments>();
            var listBond = await bondData.CombinedBondDataAsync();
            foreach (var foundBond in listBond)
            {
                var bond = new SingleModelExchangeInstruments()
                {
                    ShortName = foundBond.Security.SHORTNAME,
                    PriceChange = foundBond.Marketdata.MARKETPRICE,
                    TypeInstrument = TypeInstrument.Bond
                };
                listInstruments.Add(bond);
            }
            return listInstruments;
        }
        public async Task<List<SingleModelExchangeInstruments>> GetFundFullModelAsync()
        {
            return new List<SingleModelExchangeInstruments> { new SingleModelExchangeInstruments() { } };
        }
        public async Task<List<SingleModelExchangeInstruments>> GetFutureFullModelAsync()
        {
            return new List<SingleModelExchangeInstruments> { new SingleModelExchangeInstruments() { } };
        }
        public enum TypeInstrument
        {
            [Description("Неопределено")]
            None,
            [Description("Акции")]
            Stock,
            [Description("Валюта")]
            Currency,
            [Description("Облигации")]
            Bond
        }
    }
}
