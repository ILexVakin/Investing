using Investing.Models.ViewModels;
using Investing.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Investing.Services.Interfaces;
using System.Linq;
using System;

namespace Investing.Services.MoexData
{
    public class FuturesData
    {
         List<FuturesSecurity> listSecurities = new List<FuturesSecurity>();
         List<FuturesMarketdata> listMarketdata = new List<FuturesMarketdata>();
         IReadingMoexData moexData = new ReadingMoexData();

         List<FuturesSecurity> GetFuturesSecuritiesData(JsonElement securitiesRows)
        {
            JsonElement securities = securitiesRows[1].GetProperty("securities");
            for (int i = 0; i < securities.GetArrayLength(); i++)
            {
                var futures = new FuturesSecurity
                {
                    SECID = securities[i].GetProperty("SECID").GetString(),
                    SHORTNAME = securities[i].GetProperty("SHORTNAME").GetString(),
                    PREVPRICE = securities[i].TryGetProperty("PREVPRICE", out var prevPrice)
                                                && prevPrice.ValueKind != JsonValueKind.Null
                                    ? prevPrice.GetDecimal() : (decimal?)null,
                    SECNAME = securities[i].GetProperty("SECNAME").GetString(),
                    LATNAME = securities[i].GetProperty("LATNAME").GetString(),
                };
                listSecurities.Add(futures);
            }
            return listSecurities;
        }

        List<FuturesMarketdata> GetFuturesMarketdata(JsonElement marketdataRows)
        {
            JsonElement marketdata = marketdataRows[1].GetProperty("marketdata");

            for (int i = 0; i < marketdata.GetArrayLength(); i++)
            {
                var stock = new FuturesMarketdata
                {
                    SECID = marketdata[i].GetProperty("SECID").GetString(),
                    BOARDID = marketdata[i].TryGetProperty("BOARDID", out var boardId)
                                    && boardId.ValueKind != JsonValueKind.Null
                                    ? boardId.GetString()
                                    : null,
                    LASTTOPREVPRICE = marketdata[i].TryGetProperty("LASTTOPREVPRICE", out var lastTopPrevPrice)
                                    && lastTopPrevPrice.ValueKind != JsonValueKind.Null
                                    ? lastTopPrevPrice.GetDecimal()
                                    : (decimal?)null,
                    //SHORTNAME = marketdata[i].GetProperty("SHORTNAME").GetString(),
                    //PREVPRICE = (float?)marketdata[i].GetProperty("PREVPRICE").GetDouble()
                };
                listMarketdata.Add(stock);
            }

            return listMarketdata;
        }

        public async Task<List<Futures>> CombinedFuturesDataAsync()
        {
            var listDataFutures = await moexData.GetAllRowsByExchange("https://iss.moex.com/iss/engines/futures/markets/forts/boards/RFUD/securities.json?iss.meta=off&iss.json=extended&limit=100");

            Task getBondsData = new Task(() =>
            {
                listSecurities = GetFuturesSecuritiesData(listDataFutures);
                listMarketdata = GetFuturesMarketdata(listDataFutures);
            });
            getBondsData.RunSynchronously();


            var combinedData = listSecurities.GroupJoin(listMarketdata,
              sec => sec.SECID,
              mar => mar.SECID,
              (sec, mar) => new Futures
              {
                  Securities = sec,
                  Marketdata = mar.FirstOrDefault()
              })
              .ToList();


            return combinedData;
        }
    }
}
