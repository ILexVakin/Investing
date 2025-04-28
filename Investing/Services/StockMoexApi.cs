using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using Investing.Models;
using System.Collections.Generic;
using Investing.Models.ViewModels;
using System.Linq;
using System.Diagnostics;

namespace Investing.Services
{
    public class StockMoexApi
    {
        private readonly HttpClient _httpClient;
        List<Security> listSecurities = new List<Security>();
        List<Marketdata> listMarketdata = new List<Marketdata>();

        public StockMoexApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //сделать в дальнейшем единый метод
        private async Task<JsonElement> ReadAllRows(string url)
        {
            JsonElement root = new JsonElement();
            HttpResponseMessage response = await _httpClient.GetAsync(url);

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

        internal List<Security> GetSecurities(JsonElement securitiesRows)
        {
            JsonElement securities = securitiesRows[1].GetProperty("securities");
            // Обрабатываем данные
            for (int i = 0; i < securities.GetArrayLength(); i++)
            {
                var stock = new Security
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
        internal List<Marketdata> GetMarketdata(JsonElement marketdataRows)
        {
            JsonElement marketdata = marketdataRows[1].GetProperty("marketdata");
            // Обрабатываем данные

            for (int i = 0; i < marketdata.GetArrayLength(); i++)
            {
                var stock = new Marketdata
                {
                    SECID = marketdata[i].GetProperty("SECID").GetString(),
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

        public async Task<List<CombinedStocsVM>> GetCombinedDataAsync()
        {
            var listDataStocks = await ReadAllRows("https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities.json?iss.meta=off&iss.json=extended&limit=100");

            Task getStocksData = new Task(() =>
            {
                listSecurities = GetSecurities(listDataStocks);
                listMarketdata = GetMarketdata(listDataStocks);
            });
            getStocksData.RunSynchronously();

            // Объединяем данные по SECID и BOARDID
            var combinedData = listSecurities.Select(s => new CombinedStocsVM
            {
                Security = s,
                Marketdata = listMarketdata.FirstOrDefault(m =>
                    m.SECID == s.SECID && m.BOARDID == s.BOARDID)
            }).ToList();
            
            return combinedData;
        }


        internal async Task<List<StockMoexUnion>> UnionImageStockAsync()
        {
            var listUnion = new List<StockMoexUnion>();
            var listStocks = await GetCombinedDataAsync();
            var l = new List<StockMoexUnion>()
            {
                new StockMoexUnion
                {
                 //   SecId = listStocks
                }
            };
            return listUnion;
        }
    }
}