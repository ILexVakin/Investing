using Investing.Models;
using Investing.Services.Interfaces;
using Investing.Services.MoexData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Investing.Services
{
    public class DetailInstrument<T> : IDetailInstrument<T> where T : class, new()
    {
        StockData stockData = new StockData();
        
        async Task<T> IDetailInstrument<T>.DetailInstrument(string secId, FullModelInstrumentsMoex.TypeInstrument typeInstrument)
        {
            object a = "";
            switch (typeInstrument)
            {
                case FullModelInstrumentsMoex.TypeInstrument.Stock:
                var stocks = await GetDataModelInstrument(secId);
                   return (T)(object)stocks;
                case FullModelInstrumentsMoex.TypeInstrument.Currency:
                    break;
            }
            return (T)(object)a;
        }

        public async Task<T> GetDataModelInstrument(string secid)
        {
            var endPoint = EndPointsMoex.StockInfo.Replace("SBER", secid);
            var model = await stockData.CombinedStockDataNewAsync(endPoint);

            return (T)(object)model;
        }
    }
    public static class EndPointsMoex
    {
        //endpoint с полным описанием по инструменту
        public static string StockInfo = @"https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities/SBER.json?iss.meta=off&iss.json=extended&securities=&marketdata=&trades=&orderbook=";



        //endpoint по свече - за несколько дней
        public static string StockСandlesInfo =    @"https://iss.moex.com/iss/history/engines/stock/markets/shares/boards/TQBR/securities/SBER.json?iss.meta=off&from=2024-01-01&till=2024-12-01&limit=100";
    }
}


