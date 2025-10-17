using Investing.Services.Interfaces;
using System.Text;
using System;
using System.Threading.Tasks;
using Investing.Services.MoexData;
using Investing.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Diagnostics;

namespace Investing.Services
{
    public class CandlesData<T> : ICandlesData<T> where T : class
    {
        IReadingMoexData readingMoex = new ReadingMoexData();
        async Task<List<CandlesByDayResponse>> ICandlesData<T>.GetCandlesByDayAsync(string endpoint)
        {
            var modelInstruments = await readingMoex.GetAllRowsByExchange(endpoint);
            return await FillingCandlesData(modelInstruments);
        }

        async Task<T> ICandlesData<T>.GetCandlesByManyDaysAsync(string endpoint)
        {
            var modelInstruments = await readingMoex.GetAllRowsByExchange(endpoint);
            throw new System.NotImplementedException();
        }
        async Task<List<CandlesByDayResponse>> FillingCandlesData(JsonElement elementInstrument)
        {
            List<CandlesByDayResponse> listCandles = new List<CandlesByDayResponse>();

            if (elementInstrument.TryGetProperty("candles", out JsonElement candlesElement) &&
             candlesElement.TryGetProperty("data", out JsonElement dataElement) &&
             dataElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var element in dataElement.EnumerateArray())
                {
                    try
                    {
                        var hour = new CandlesByDayResponse()
                        {
                            Open = element[0].GetDecimal(),
                            Close = element[1].GetDecimal(),
                            High = element[2].GetDecimal(),
                            Low = element[3].GetDecimal(),
                            Value = element[4].GetDecimal(),
                            Volume = element[5].GetDecimal(),
                            Begin = element[6].ToString(),
                            End = element[7].ToString()
                        };
                        listCandles.Add(hour);
                    }
                    catch(Exception ex) 
                    {
                        Debug.WriteLine(ex);
                    }

                }
            }

            await Task.Delay(1000);
            return listCandles;
        }

    }
}
