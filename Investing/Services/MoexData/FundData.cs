using Investing.Models.ViewModels;
using Investing.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Investing.Services.Interfaces;
using System.Linq;

namespace Investing.Services.MoexData
{
    public class FundData
    {
        IReadingMoexData moexData = new ReadingMoexData();

        public async Task<List<CombinedStocsVM>> GetFundsAsync()
        {
            var listData = await moexData.GetAllRowsByExchange("https://iss.moex.com/iss/engines/stock/markets/etf/boards/TQTF/securities.json?iss.meta=off&iss.json=extended&limit=100");
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
    }
}
