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
                    SECID = securities[i].GetProperty("SECID").GetString(),
                    SHORTNAME = securities[i].GetProperty("SHORTNAME").GetString(),
                    PREVPRICE = securities[i].TryGetProperty("PREVPRICE", out var prevPrice)
                    && prevPrice.ValueKind != JsonValueKind.Null
                                    ? prevPrice.GetSingle()
                                    : null,
                    LOTSIZE = securities[i].GetProperty("LOTSIZE").GetInt32(),
                    SECNAME = securities[i].GetProperty("SECNAME").GetString(),
                    FACEUNIT = securities[i].GetProperty("FACEUNIT").GetString()
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
                    SECID = marketdata[i].GetProperty("SECID").GetString(),
                    LAST = marketdata[i].TryGetProperty("LAST", out var last)
                    && last.ValueKind != JsonValueKind.Null
                                    ? last.GetDecimal()
                                    : (decimal?)null,
                    OPEN = marketdata[i].TryGetProperty("OPEN", out var open)
                    && open.ValueKind != JsonValueKind.Null
                                    ? open.GetSingle()
                                    : null,
                    LASTTOPREVPRICE = marketdata[i].TryGetProperty("LASTTOPREVPRICE", out var lastPrice)
                    && lastPrice.ValueKind != JsonValueKind.Null
                                    ? lastPrice.GetDecimal()
                                    : null
                });
            }

            return listMarketdata;
        }

        public async Task<List<Currency>> CombinedCurrencyDataAsync()
        {
            var listCurrency = await moexData.GetAllRowsByExchange("https://iss.moex.com/iss/engines/currency/markets/selt/boards/CETS/securities.json?iss.meta=off&iss.json=extended&limit=100");
            Task getCurrencyData = new Task(() =>
            {
                listSecurities = GetCurrencySecuritiesData(listCurrency);
                listMarketdata = GetCurrencyMarketdata(listCurrency);
            });
            getCurrencyData.RunSynchronously();

            var combinedData = listSecurities.GroupJoin(listMarketdata,
             sec => sec.SECID,
             mar => mar.SECID,
             (sec, mar) => new Currency
             {
                 Security = sec,
                 Marketdata = mar.FirstOrDefault()
             })
             .ToList();

            return combinedData;
        }
    }
}
