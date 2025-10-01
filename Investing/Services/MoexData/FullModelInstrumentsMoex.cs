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
        public async Task<List<SingleModelExchangeInstruments>> GetStockFullModelAsync()
        {
            List<SingleModelExchangeInstruments> listInstruments = new List<SingleModelExchangeInstruments>();
            var listStocks = await StockData.CombinedStockDataAsync();
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
            var listCurrency = await CurrencyData.CombinedCurrencyDataAsync();

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
            var listBond = await BondData.CombinedBondDataAsync();
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
            List<SingleModelExchangeInstruments> listInstruments = new List<SingleModelExchangeInstruments>();
            var listFund = await FundData.CombinedFundDataAsync();
            foreach (var fondFund in listFund)
            {
                var bond = new SingleModelExchangeInstruments()
                {
                    ShortName = fondFund.Security.SHORTNAME,
                    PriceChange = fondFund.Marketdata.MARKETPRICE,
                    TypeInstrument = TypeInstrument.Fund
                };
                listInstruments.Add(bond);
            }
            return listInstruments;
        }
        public async Task<List<SingleModelExchangeInstruments>> GetFuturesFullModelAsync()
        {
            List<SingleModelExchangeInstruments> listInstruments = new List<SingleModelExchangeInstruments>();
            var listFutures = await FuturesData.CombinedFuturesDataAsync();
            foreach (var fundBond in listFutures)
            {
                var bond = new SingleModelExchangeInstruments()
                {
                    //ShortName = fundBond.Security.SHORTNAME,
                    //PriceChange = fundBond.Marketdata.MARKETPRICE,
                    TypeInstrument = TypeInstrument.Futures
                };
                listInstruments.Add(bond);
            }
            return listInstruments;
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
            Bond,
            [Description("Фонды")]
            Fund,
            [Description("Фьючерсы")]
            Futures
        }
    }
}
