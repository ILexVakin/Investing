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
                    LATNAME = securities[i].GetProperty("LATNAME").GetString(),
                    PREVLEGALCLOSEPRICE = (float?)securities[i].GetProperty("PREVLEGALCLOSEPRICE").GetDouble()
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
                                    ? boardId.GetString()
                                    : null,
                    MARKETPRICE2 = marketdata[i].TryGetProperty("MARKETPRICE2", out var marketPrice)
                                    && marketPrice.ValueKind != JsonValueKind.Null
                                    ? marketPrice.GetDecimal()
                                    : (decimal?)null,
                    TRADINGSTATUS = marketdata[i].GetProperty("TRADINGSTATUS").GetString(),
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

        public async Task<List<CombinedStocsVM>> CombinedStockDataAsync()
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
              (sec, mar) => new CombinedStocsVM
              { 
                  Security = sec,
                  Marketdata = mar.FirstOrDefault()
              })
              .ToList();


            return combinedData;
        }
    }
}
