using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using Investing.Models;
using System.Collections.Generic;
using Investing.Models.ViewModels;
using System.Linq;
using System.Diagnostics;
using Investing.Services.Interfaces;

namespace Investing.Services
{
    public class ReadingMoexData : IReadingMoexData
    {
        public async Task<JsonElement> GetAllRowsByExchange(string url)
        {
            HttpClient httpClient = new HttpClient();
            JsonElement root = new JsonElement();
            HttpResponseMessage response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();

                using (JsonDocument doc = JsonDocument.Parse(responseData))
                {
                    root = doc.RootElement.Clone();
                }
            }
            return root;
        }

        public async Task<List<CombinedStocsVM>> GetBondsAsync()
        {
            var listData = await GetAllRowsByExchange("https://iss.moex.com/iss/engines/stock/markets/bonds/boards/TQCB/securities.json?iss.meta=off&iss.json=extended&limit=100");
            var bondSecurities = new List<BondSecurity>();
            var bondMarketdata = new List<BondMarketdata>();

            JsonElement securities = listData[1].GetProperty("securities");
            for (int i = 0; i < securities.GetArrayLength(); i++)
            {
                bondSecurities.Add(new BondSecurity
                {
                    SECID = securities[i].GetProperty("SECID").GetString(),
                    SHORTNAME = securities[i].GetProperty("SHORTNAME").GetString(),
                    PREVPRICE = (float?)securities[i].GetProperty("PREVPRICE").GetDouble(),
                    BOARDID = securities[i].TryGetProperty("BOARDID", out var board) && board.ValueKind != JsonValueKind.Null ? board.GetString() : null,
                    ISIN = securities[i].TryGetProperty("ISIN", out var isin) && isin.ValueKind != JsonValueKind.Null ? isin.GetString() : null,
                    FACEVALUE = securities[i].TryGetProperty("FACEVALUE", out var fv) && fv.ValueKind != JsonValueKind.Null ? (float)fv.GetDouble() : 0,
                    FACEUNIT = securities[i].TryGetProperty("FACEUNIT", out var fu) && fu.ValueKind != JsonValueKind.Null ? fu.GetString() : null
                });
            }

            JsonElement marketdata = listData[1].GetProperty("marketdata");
            for (int i = 0; i < marketdata.GetArrayLength(); i++)
            {
                bondMarketdata.Add(new BondMarketdata
                {
                    SECID = marketdata[i].GetProperty("SECID").GetString(),
                    BOARDID = marketdata[i].TryGetProperty("BOARDID", out var board) && board.ValueKind != JsonValueKind.Null ? board.GetString() : null,
                    MARKETPRICE2 = marketdata[i].TryGetProperty("MARKETPRICE2", out var mp2) && mp2.ValueKind != JsonValueKind.Null ? mp2.GetDecimal() : (decimal?)null,
                    LAST = marketdata[i].TryGetProperty("LAST", out var last) && last.ValueKind != JsonValueKind.Null ? last.GetDecimal() : null,
                    YIELD = marketdata[i].TryGetProperty("YIELD", out var yld) && yld.ValueKind != JsonValueKind.Null ? (decimal?)yld.GetDecimal() : null,
                    TRADINGSTATUS = marketdata[i].TryGetProperty("TRADINGSTATUS", out var ts) && ts.ValueKind != JsonValueKind.Null ? ts.GetString() : null
                });
            }

            var result = bondSecurities.Select(s => new CombinedStocsVM
            {
                Security = new SecurityStock { SECID = s.SECID, SHORTNAME = s.SHORTNAME, PREVPRICE = s.PREVPRICE, BOARDID = s.BOARDID },
                Marketdata = new MarketdataStock { SECID = s.SECID, BOARDID = s.BOARDID, MARKETPRICE2 = bondMarketdata.FirstOrDefault(m => m.SECID == s.SECID && m.BOARDID == s.BOARDID)?.MARKETPRICE2, LAST = bondMarketdata.FirstOrDefault(m => m.SECID == s.SECID && m.BOARDID == s.BOARDID)?.LAST, TRADINGSTATUS = bondMarketdata.FirstOrDefault(m => m.SECID == s.SECID && m.BOARDID == s.BOARDID)?.TRADINGSTATUS }
            }).ToList();

            return result;
        }

        public async Task<List<CombinedStocsVM>> GetFundsAsync()
        {
            var listData = await GetAllRowsByExchange("https://iss.moex.com/iss/engines/stock/markets/etf/boards/TQTF/securities.json?iss.meta=off&iss.json=extended&limit=100");
            var fundSecurities = new List<FundSecurity>();
            var fundMarketdata = new List<FundMarketdata>();

            JsonElement securities = listData[1].GetProperty("securities");
            for (int i = 0; i < securities.GetArrayLength(); i++)
            {
                fundSecurities.Add(new FundSecurity
                {
                    SECID = securities[i].GetProperty("SECID").GetString(),
                    SHORTNAME = securities[i].GetProperty("SHORTNAME").GetString(),
                    PREVPRICE = (float?)securities[i].GetProperty("PREVPRICE").GetDouble(),
                    BOARDID = securities[i].TryGetProperty("BOARDID", out var board) && board.ValueKind != JsonValueKind.Null ? board.GetString() : null
                });
            }

            JsonElement marketdata = listData[1].GetProperty("marketdata");
            for (int i = 0; i < marketdata.GetArrayLength(); i++)
            {
                fundMarketdata.Add(new FundMarketdata
                {
                    SECID = marketdata[i].GetProperty("SECID").GetString(),
                    BOARDID = marketdata[i].TryGetProperty("BOARDID", out var board) && board.ValueKind != JsonValueKind.Null ? board.GetString() : null,
                    MARKETPRICE2 = marketdata[i].TryGetProperty("MARKETPRICE2", out var mp2) && mp2.ValueKind != JsonValueKind.Null ? mp2.GetDecimal() : (decimal?)null,
                    LAST = marketdata[i].TryGetProperty("LAST", out var last) && last.ValueKind != JsonValueKind.Null ? last.GetDecimal() : null,
                    TRADINGSTATUS = marketdata[i].TryGetProperty("TRADINGSTATUS", out var ts) && ts.ValueKind != JsonValueKind.Null ? ts.GetString() : null
                });
            }

            var result = fundSecurities.Select(s => new CombinedStocsVM
            {
                Security = new SecurityStock { SECID = s.SECID, SHORTNAME = s.SHORTNAME, PREVPRICE = s.PREVPRICE, BOARDID = s.BOARDID },
                Marketdata = new MarketdataStock { SECID = s.SECID, BOARDID = s.BOARDID, MARKETPRICE2 = fundMarketdata.FirstOrDefault(m => m.SECID == s.SECID && m.BOARDID == s.BOARDID)?.MARKETPRICE2, LAST = fundMarketdata.FirstOrDefault(m => m.SECID == s.SECID && m.BOARDID == s.BOARDID)?.LAST, TRADINGSTATUS = fundMarketdata.FirstOrDefault(m => m.SECID == s.SECID && m.BOARDID == s.BOARDID)?.TRADINGSTATUS }
            }).ToList();

            return result;
        }

        public async Task<List<CombinedStocsVM>> GetFuturesAsync()
        {
            var listData = await GetAllRowsByExchange("https://iss.moex.com/iss/engines/futures/markets/forts/boards/RFUD/securities.json?iss.meta=off&iss.json=extended&limit=100");
            var futureSecurities = new List<FutureSecurity>();
            var futureMarketdata = new List<FutureMarketdata>();

            JsonElement securities = listData[1].GetProperty("securities");
            for (int i = 0; i < securities.GetArrayLength(); i++)
            {
                futureSecurities.Add(new FutureSecurity
                {
                    SECID = securities[i].GetProperty("SECID").GetString(),
                    SHORTNAME = securities[i].GetProperty("SHORTNAME").GetString(),
                    BOARDID = securities[i].TryGetProperty("BOARDID", out var board) && board.ValueKind != JsonValueKind.Null ? board.GetString() : null
                });
            }

            JsonElement marketdata = listData[1].GetProperty("marketdata");
            for (int i = 0; i < marketdata.GetArrayLength(); i++)
            {
                futureMarketdata.Add(new FutureMarketdata
                {
                    SECID = marketdata[i].GetProperty("SECID").GetString(),
                    BOARDID = marketdata[i].TryGetProperty("BOARDID", out var board) && board.ValueKind != JsonValueKind.Null ? board.GetString() : null,
                    LAST = marketdata[i].TryGetProperty("LAST", out var last) && last.ValueKind != JsonValueKind.Null ? last.GetDecimal() : null,
                    TRADINGSTATUS = marketdata[i].TryGetProperty("TRADINGSTATUS", out var ts) && ts.ValueKind != JsonValueKind.Null ? ts.GetString() : null
                });
            }

            var result = futureSecurities.Select(s => new CombinedStocsVM
            {
                Security = new SecurityStock { SECID = s.SECID, SHORTNAME = s.SHORTNAME, BOARDID = s.BOARDID },
                Marketdata = new MarketdataStock { SECID = s.SECID, BOARDID = s.BOARDID, LAST = futureMarketdata.FirstOrDefault(m => m.SECID == s.SECID && m.BOARDID == s.BOARDID)?.LAST, TRADINGSTATUS = futureMarketdata.FirstOrDefault(m => m.SECID == s.SECID && m.BOARDID == s.BOARDID)?.TRADINGSTATUS }
            }).ToList();

            return result;
        }


        //internal async Task<List<StockMoexUnion>> UnionImageStockAsync()
        //{
        //    var listUnion = new List<StockMoexUnion>();
        //    var listStocks = await CombinedStockDataAsync();
        //    var l = new List<StockMoexUnion>()
        //    {
        //        new StockMoexUnion
        //        {
        //         //   SecId = listStocks
        //        }
        //    };
        //    return listUnion;
        //}
    }
}