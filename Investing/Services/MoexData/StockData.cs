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
        List<Stocks> stocks = new List<Stocks>();   
        List<SecurityStock> listSecurities = new List<SecurityStock>();
        List<MarketdataStock> listMarketdata = new List<MarketdataStock>();
        IReadingMoexData moexData = new ReadingMoexData();
        List<SecurityStock> GetStockSecuritiesData(JsonElement securitiesRows)
        {
            JsonElement securities = securitiesRows[1].GetProperty("securities");
            
            for (int i = 0; i < securities.GetArrayLength(); i++)
            {
                var stock = new SecurityStock
                {
                    SECID = securities[i].GetProperty("SECID").GetString(),
                    SHORTNAME = securities[i].GetProperty("SHORTNAME").GetString(),
                    PREVPRICE = (float?)securities[i].GetProperty("PREVPRICE").GetDouble(),
                    LOTSIZE = securities[i].GetProperty("LOTSIZE").GetInt32(),
                    FACEVALUE = (float)securities[i].GetProperty("FACEVALUE").GetDouble(),
                    SECNAME = securities[i].GetProperty("SECNAME").GetString(),
                    ISIN = securities[i].GetProperty("ISIN").GetString(),
                    LATNAME = securities[i].GetProperty("LATNAME").GetString(),
                    PREVLEGALCLOSEPRICE = securities[i].GetProperty("PREVLEGALCLOSEPRICE").GetSingle(),
                    ISSUESIZE = securities[i].GetProperty("ISSUESIZE").GetDouble(),
                };
                listSecurities.Add(stock);
            }
            return listSecurities;
        }
        List<MarketdataStock> GetStockMarketdata(JsonElement marketdataRows)
        {
            JsonElement marketdata = marketdataRows[1].GetProperty("marketdata");

            for (int i = 0; i < marketdata.GetArrayLength(); i++)
            {
                var stock = new MarketdataStock
                {
                    SECID = marketdata[i].GetProperty("SECID").GetString(),
                    BOARDID = marketdata[i].TryGetProperty("BOARDID", out var boardId)
                                    && boardId.ValueKind != JsonValueKind.Null
                                    ? boardId.GetString(): null,
                    MARKETPRICE2 = marketdata[i].TryGetProperty("MARKETPRICE2", out var marketPrice)
                                    && marketPrice.ValueKind != JsonValueKind.Null
                                    ? marketPrice.GetDecimal(): null,
                    TRADINGSTATUS = marketdata[i].GetProperty("TRADINGSTATUS").GetString(),
                    LASTTOPREVPRICE = marketdata[i].TryGetProperty("LASTTOPREVPRICE", out var lastTopPrevPrice)
                                    && lastTopPrevPrice.ValueKind != JsonValueKind.Null
                                    ? lastTopPrevPrice.GetDecimal(): null,
                    OFFER = marketdata[i].TryGetProperty("OFFER", out var offer)
                                    && offer.ValueKind != JsonValueKind.Null
                                    ? offer.GetSingle() : null,
                    OPEN = marketdata[i].TryGetProperty("OPEN", out var open)
                                    && open.ValueKind != JsonValueKind.Null
                                    ? open.GetSingle() : null,
                    LOW = marketdata[i].TryGetProperty("LOW", out var low)
                                    && low.ValueKind != JsonValueKind.Null
                                    ? low.GetSingle() : null,
                    HIGH = marketdata[i].TryGetProperty("HIGH", out var high)
                                    && high.ValueKind != JsonValueKind.Null
                                    ? high.GetSingle() : null,
                    NUMTRADES = marketdata[i].TryGetProperty("NUMTRADES", out var numtrades)
                                    && lastTopPrevPrice.ValueKind != JsonValueKind.Null
                                    ? numtrades.GetInt64() : null
                };
                listMarketdata.Add(stock);
            }

            return listMarketdata;
        }

        public async Task<List<Stocks>> CombinedStockDataAsync()
        {
            var listDataStocks = await moexData.GetAllRowsByExchange("https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities.json?iss.meta=off&iss.json=extended&limit=100");

            Task getStocksData = new Task(() =>
            {
                listSecurities = GetStockSecuritiesData(listDataStocks);
                listMarketdata = GetStockMarketdata(listDataStocks);
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

            Task getStocksData = new Task(() =>
            {
                listSecurities = GetStockSecuritiesData(listDataStocks);
                listMarketdata = GetStockMarketdata(listDataStocks);
            });
            getStocksData.RunSynchronously();


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
