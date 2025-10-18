using Investing.Data;
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

        IIconCompany iconCompany = new IconCompany(); 
        StockData stockData = new StockData();
        CurrencyData currencyData = new CurrencyData();
        BondData bondData = new BondData();
        FundData  fundData = new FundData();
        FuturesData futuresData = new FuturesData();

        public async Task<List<SingleModelExchangeInstruments>> GetStockFullModelAsync()
        {
            List<SingleModelExchangeInstruments> listInstruments = new List<SingleModelExchangeInstruments>();
            var listStocks = await stockData.CombinedStockDataAsync();
            foreach (var foundStock in listStocks)
            {
                var stock = new SingleModelExchangeInstruments()
                {
                    SecName = foundStock.Security.SECNAME,
                    SecId = foundStock.Security.SECID,
                    Isin = foundStock.Security.ISIN,
                    PriceChange = foundStock.Marketdata.MARKETPRICE,
                    TypeInstrument = TypeInstrument.Stock,
                    ImageIcon = await iconCompany.GetIconCompany(foundStock.Security.ISIN)
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
                    SecName = foundCurrency.Security.SECNAME,
                    SecId = foundCurrency.Security.SECID,
                    PriceChange = foundCurrency.Marketdata.MARKETPRICE,
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
                    SecName = foundBond.Security.SECNAME,
                    SecId = foundBond.Security.SECID,
                    Isin = foundBond.Security.ISIN,
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
            var listFund = await fundData.CombinedFundDataAsync();
            foreach (var fondFund in listFund)
            {
                var bond = new SingleModelExchangeInstruments()
                {
                    SecName = fondFund.Security.SECNAME,
                    SecId = fondFund.Security.SECID,
                    Isin = fondFund.Security.ISIN,
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
            var listFutures = await futuresData.CombinedFuturesDataAsync();
            foreach (var fundBond in listFutures)
            {
                var bond = new SingleModelExchangeInstruments()
                {
                    //ShortName = fundBond.Security.SHORTNAME,
                    //PriceChange = fundBond.Marketdata.MARKETPRICE,
                    SecName = fundBond.Securities.SECNAME,
                    SecId = fundBond.Securities.SECID,
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
