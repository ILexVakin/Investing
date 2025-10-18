using Investing.Models.ViewModels;
using Investing.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Linq;
using Investing.Services.Interfaces;

namespace Investing.Services.MoexData
{
    public class StockData
    {
        IIconCompany iconCompany = new IconCompany();
        List<Stocks> stocks = new List<Stocks>();   
        List<SecurityStock> listSecurities = new List<SecurityStock>();
        List<MarketdataStock> listMarketdata = new List<MarketdataStock>();
        IReadingMoexData moexData = new ReadingMoexData();
        async Task<List<SecurityStock>> GetStockSecuritiesData(JsonElement securitiesRows)
        {
            JsonElement securities = securitiesRows[1].GetProperty("securities");
            
              var task = securities.EnumerateArray()
                 .Select(async sec =>
                 {
                     return new SecurityStock
                     {
                         SECID = sec.GetProperty("SECID").GetString(),
                         SHORTNAME = sec.GetProperty("SHORTNAME").GetString(),
                         PREVPRICE = (float?)sec.GetProperty("PREVPRICE").GetDouble(),
                         LOTSIZE = sec.GetProperty("LOTSIZE").GetInt32(),
                         FACEVALUE = (float)sec.GetProperty("FACEVALUE").GetDouble(),
                         SECNAME = sec.GetProperty("SECNAME").GetString(),
                         ISIN = sec.GetProperty("ISIN").GetString(),
                         LATNAME = sec.GetProperty("LATNAME").GetString(),
                         PREVLEGALCLOSEPRICE = sec.GetProperty("PREVLEGALCLOSEPRICE").GetSingle(),
                         ISSUESIZE = sec.GetProperty("ISSUESIZE").GetDouble(),
                         IconCompany = await iconCompany.GetIconCompany(sec.GetProperty("ISIN").GetString())
                     };
                 }).ToList();
            var results = await Task.WhenAll(task);
            return results.ToList();
        }
       async Task<List<MarketdataStock>> GetStockMarketdata(JsonElement marketdataRows)
        {
            JsonElement marketdata = marketdataRows[1].GetProperty("marketdata");

            var task = marketdata.EnumerateArray()
                 .Select(async mar =>
                 {
                     return new MarketdataStock
                     {
                         SECID = mar.GetProperty("SECID").GetString(),
                         BOARDID = mar.TryGetProperty("BOARDID", out var boardId)
                                    && boardId.ValueKind != JsonValueKind.Null
                                    ? boardId.GetString() : null,
                         MARKETPRICE2 = mar.TryGetProperty("MARKETPRICE2", out var marketPrice)
                                    && marketPrice.ValueKind != JsonValueKind.Null
                                    ? marketPrice.GetDecimal() : null,
                         TRADINGSTATUS = mar.GetProperty("TRADINGSTATUS").GetString(),
                         LASTTOPREVPRICE = mar.TryGetProperty("LASTTOPREVPRICE", out var lastTopPrevPrice)
                                    && lastTopPrevPrice.ValueKind != JsonValueKind.Null
                                    ? lastTopPrevPrice.GetDecimal() : null,
                         OFFER = mar.TryGetProperty("OFFER", out var offer)
                                    && offer.ValueKind != JsonValueKind.Null
                                    ? offer.GetSingle() : null,
                         OPEN = mar.TryGetProperty("OPEN", out var open)
                                    && open.ValueKind != JsonValueKind.Null
                                    ? open.GetSingle() : null,
                         LOW = mar.TryGetProperty("LOW", out var low)
                                    && low.ValueKind != JsonValueKind.Null
                                    ? low.GetSingle() : null,
                         HIGH = mar.TryGetProperty("HIGH", out var high)
                                    && high.ValueKind != JsonValueKind.Null
                                    ? high.GetSingle() : null,
                         NUMTRADES = mar.TryGetProperty("NUMTRADES", out var numtrades)
                                    && lastTopPrevPrice.ValueKind != JsonValueKind.Null
                                    ? numtrades.GetInt64() : null
                     };
                 }).ToList();

            var results = await Task.WhenAll(task);
            return results.ToList();
        }

        public async Task<List<Stocks>> CombinedStockDataAsync()
        {
            var listDataStocks = await moexData.GetAllRowsByExchange("https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities.json?iss.meta=off&iss.json=extended&limit=100");

            Task getStocksData = new Task(async () =>
            {
                listSecurities = await GetStockSecuritiesData(listDataStocks);
                listMarketdata = await GetStockMarketdata(listDataStocks);
            });
            getStocksData.RunSynchronously();


              var combinedData = listSecurities.GroupJoin(listMarketdata,
              sec => sec.SECID,
              mar => mar.SECID,
              (sec, mar) => new Stocks
              { 
                  Security = sec,
                  Marketdata = mar.FirstOrDefault()
              })
              .ToList();


            return combinedData;
        }

        public async Task<Stocks> CombinedStockDataNewAsync(string endPointMoex)
        {
            var listDataStocks = await moexData.GetAllRowsByExchange(endPointMoex);

            var securitiesTask = GetStockSecuritiesData(listDataStocks);
            var marketdataTask = GetStockMarketdata(listDataStocks);

            await Task.WhenAll(securitiesTask, marketdataTask);
            listSecurities = securitiesTask.Result;
            listMarketdata = marketdataTask.Result;

            var combinedData = listSecurities.Join(listMarketdata,
              sec => sec.SECID,
              mar => mar.SECID,
              (sec, mar) => new Stocks
              {
                  Security = sec,
                  Marketdata = mar
              });


            return combinedData.FirstOrDefault();
        }
    }
}
