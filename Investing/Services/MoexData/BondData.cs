using Investing.Models.ViewModels;
using Investing.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using Investing.Services.Interfaces;
using System;

namespace Investing.Services.MoexData
{
    public class BondData
    {
         List<BondSecurity> listSecurities = new List<BondSecurity>();
         List<BondMarketdata> listMarketdata = new List<BondMarketdata>();
         IReadingMoexData moexData = new ReadingMoexData();

        List<BondSecurity> GetBondSecuritiesData(JsonElement securitiesRows)
        {
            JsonElement securities = securitiesRows[1].GetProperty("securities");
            for (int i = 0; i < securities.GetArrayLength(); i++)
            {
                var stock = new BondSecurity
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
                                    ? faceValue.GetDouble() : (double?)null,
                    SECNAME = securities[i].GetProperty("SECNAME").GetString(),
                    ISIN = securities[i].GetProperty("ISIN").GetString(),
                    LATNAME = securities[i].GetProperty("LATNAME").GetString(),
                    PREVLEGALCLOSEPRICE = securities[i].TryGetProperty("PREVLEGALCLOSEPRICE", out var closePrice)
                    && closePrice.ValueKind != JsonValueKind.Null
                                    ? closePrice.GetDecimal() : (decimal?)null,
                };
                listSecurities.Add(stock);
            }
            return listSecurities;
        }
        List<BondMarketdata> GetBondMarketdata(JsonElement marketdataRows)
        {
            JsonElement marketdata = marketdataRows[1].GetProperty("marketdata");

            for (int i = 0; i < marketdata.GetArrayLength(); i++)
            {
                var stock = new BondMarketdata
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

        public async Task<List<BondItemVM>> CombinedBondDataAsync()
        {
            var listDataBond = await moexData.GetAllRowsByExchange("https://iss.moex.com/iss/engines/stock/markets/bonds/boards/TQCB/securities.json?iss.meta=off&iss.json=extended&limit=100");

            Task getBondsData = new Task(() =>
            {
                listSecurities = GetBondSecuritiesData(listDataBond);
                listMarketdata = GetBondMarketdata(listDataBond);
            });
            getBondsData.RunSynchronously();


            var combinedData = listSecurities.GroupJoin(listMarketdata,
              sec => sec.SECID,
              mar => mar.SECID,
              (sec, mar) => new BondItemVM
              {
                  Security = sec,
                  Marketdata = mar.FirstOrDefault()
              })
              .ToList();


            return combinedData;
        }
    }
}
