using Investing.Models;
using Investing.Models.ViewModels;
using Investing.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Investing.Services.MoexData
{
    public class CurrencyData
    {
         List<CurrencySecurity> listSecurities = new List<CurrencySecurity>();
         List<CurrencyMarketdata> listMarketdata = new List<CurrencyMarketdata>();
         IReadingMoexData moexData = new ReadingMoexData();

        List<CurrencySecurity> GetCurrencySecuritiesData(JsonElement securitiesRows)
        {
            JsonElement securities = securitiesRows[1].GetProperty("securities");
            for (int i = 0; i < securities.GetArrayLength(); i++)
            {
                listSecurities.Add(new CurrencySecurity
                {
                    SecId = securities[i].GetProperty("SECID").GetString(),
                    ShortName = securities[i].GetProperty("SHORTNAME").GetString(),
                    PrevPrice = securities[i].TryGetProperty("PREVPRICE", out var prevPrice)
                    && prevPrice.ValueKind != JsonValueKind.Null
                                    ? prevPrice.GetDecimal()
                                    : (decimal?)null,
                    LotSize = securities[i].GetProperty("LOTSIZE").GetInt32()
                });
            }
            return listSecurities;
        }

        List<CurrencyMarketdata> GetCurrencyMarketdata(JsonElement marketdataRows)
        {
            JsonElement marketdata = marketdataRows[1].GetProperty("marketdata");
            for (int i = 0; i < marketdata.GetArrayLength(); i++)
            {
                listMarketdata.Add(new CurrencyMarketdata
                {
                    SecId = marketdata[i].GetProperty("SECID").GetString(),
                    Last = marketdata[i].TryGetProperty("LAST", out var last)
                    && last.ValueKind != JsonValueKind.Null
                                    ? last.GetDecimal()
                                    : (decimal?)null,
                    Open = marketdata[i].TryGetProperty("OPEN", out var open)
                    && open.ValueKind != JsonValueKind.Null
                                    ? open.GetDecimal()
                                    : (decimal?)null
                });
            }

            return listMarketdata;
        }

        public async Task<List<CombinedCurrencyVM>> CombinedCurrencyDataAsync()
        {
            var listCurrency = await moexData.GetAllRowsByExchange("https://iss.moex.com/iss/engines/currency/markets/selt/boards/CETS/securities.json?iss.meta=off&iss.json=extended&limit=100");
            Task getCurrencyData = new Task(() =>
            {
                listSecurities = GetCurrencySecuritiesData(listCurrency);
                listMarketdata = GetCurrencyMarketdata(listCurrency);
            });
            getCurrencyData.RunSynchronously();

            var combinedData = listSecurities.GroupJoin(listMarketdata,
             sec => sec.SecId,
             mar => mar.SecId,
             (sec, mar) => new CombinedCurrencyVM
             {
                 Security = sec,
                 Marketdata = mar.FirstOrDefault()
             })
             .ToList();

            return combinedData;
        }
    }
}
