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
    public static class FundData
    {
        static List<FundSecurity> listSecurities = new List<FundSecurity>();
        static List<FundMarketdata> listMarketdata = new List<FundMarketdata>();
        static IReadingMoexData moexData = new ReadingMoexData();

        static List<FundSecurity> GetFundSecuritiesData(JsonElement securitiesRows)
        {
            JsonElement securities = securitiesRows[1].GetProperty("securities");
            for (int i = 0; i < securities.GetArrayLength(); i++)
            {
                var fund = new FundSecurity
                {
                    SECID = securities[i].GetProperty("SECID").GetString(),
                    SHORTNAME = securities[i].GetProperty("SHORTNAME").GetString(),
                    PREVPRICE = securities[i].TryGetProperty("PREVPRICE", out var prevPrice)
                                                && prevPrice.ValueKind != JsonValueKind.Null
                                    ? prevPrice.GetDecimal() : (decimal?)null,
                    LOTSIZE = securities[i].TryGetProperty("LOTSIZE", out var lotSize)
                    && lotSize.ValueKind != JsonValueKind.Null
                                    ? lotSize.GetInt32() : (Int32?)null,
                    FACEVALUE = securities[i].TryGetProperty("FACEVALUE", out var faceValue)
                    && faceValue.ValueKind != JsonValueKind.Null
                                    ? faceValue.GetInt32() : (Int32?)null,
                    SECNAME = securities[i].GetProperty("SECNAME").GetString(),
                    LATNAME = securities[i].GetProperty("LATNAME").GetString(),
                    PREVLEGALCLOSEPRICE = securities[i].TryGetProperty("PREVLEGALCLOSEPRICE", out var closePrice)
                    && closePrice.ValueKind != JsonValueKind.Null
                                    ? closePrice.GetDecimal() : (decimal?)null,
                };
                listSecurities.Add(fund);
            }
            return listSecurities;
        }

        static List<FundMarketdata> GetFundMarketdata(JsonElement marketdataRows)
        {
            JsonElement marketdata = marketdataRows[1].GetProperty("marketdata");

            for (int i = 0; i < marketdata.GetArrayLength(); i++)
            {
                var fund = new FundMarketdata
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
                listMarketdata.Add(fund);
            }

            return listMarketdata;
        }
        public static async Task<List<Funds>> CombinedFundDataAsync()
        {
            var listDataFund = await moexData.GetAllRowsByExchange("https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQTF/securities.json?iss.meta=off&iss.json=extended&limit=100");

            Task getFundsData = new Task(() =>
            {
                listSecurities = GetFundSecuritiesData(listDataFund);
                listMarketdata = GetFundMarketdata(listDataFund);
            });
            getFundsData.RunSynchronously();


            var combinedData = listSecurities.GroupJoin(listMarketdata,
              sec => sec.SECID,
              mar => mar.SECID,
              (sec, mar) => new Funds
              {
                  Security = sec,
                  Marketdata = mar.FirstOrDefault()
              })
              .ToList();


            return combinedData;
        }
    }
}
