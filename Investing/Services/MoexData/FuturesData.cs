using Investing.Models.ViewModels;
using Investing.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Investing.Services.Interfaces;
using System.Linq;

namespace Investing.Services.MoexData
{
    public class FuturesData
    {
        IReadingMoexData moexData = new ReadingMoexData();
        public async Task<List<CombinedStocsVM>> GetFuturesAsync()
        {
            var listData = await moexData.GetAllRowsByExchange("https://iss.moex.com/iss/engines/futures/markets/forts/boards/RFUD/securities.json?iss.meta=off&iss.json=extended&limit=100");
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
    }
}
