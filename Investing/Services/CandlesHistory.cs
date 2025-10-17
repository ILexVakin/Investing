using Investing.Models;
using Investing.Models.ViewModels;
using Investing.Services.Interfaces;
using Investing.Services.MoexData;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investing.Services
{
    public class CandlesHistory<T> : ICandlesHistory<T> where T : class, new()
    {
        ICandlesData<T> _candlesData;
        public CandlesHistory(ICandlesData<T> candlesData)
        {
            _candlesData = candlesData;
        }
        async Task<List<T>> ICandlesHistory<T>.GetHistoryInstrumentMoreDaysAsync(CandlesRequest model)
        {
            string path = await ConvertationEndpointCandlesAsync(model);
            var request = await _candlesData.GetCandlesByManyDaysAsync(path);
            return (List<T>)(object)request;
        }
        
        async Task<List<CandlesByDayResponse>> ICandlesHistory<T>.GetHistoryInstrumentOneDayAsync(CandlesRequest model)
        {
            string path = await ConvertationEndpointCandlesAsync(model);
            return await _candlesData.GetCandlesByDayAsync(path);
        }

        async Task<string> ConvertationEndpointCandlesAsync(CandlesRequest model)
        {
            var (engine, marketCode) = await DefineEngineMarketCode(model.SecId);
            var startDate = model.DateStart.ToString("yyyy-MM-dd");
            var overDate = model.DateOver.ToString("yyyy-MM-dd");
            return $"https://iss.moex.com/iss/engines/{engine}/markets/{marketCode}/securities/{model.SecId}/candles.json?interval=60&from={startDate}&till={overDate}";  
        }
        async Task<(string, string)> DefineEngineMarketCode(string secId)
        {
            if (secId.Length == 4 &&
            secId.Substring(0, 2).All(char.IsLetter) &&
            secId.Substring(2, 2).All(char.IsDigit))
            {
                return ("futures", "forts");
            }

            if (secId.Contains("TOD") || secId.Contains("RUB"))
            {
                return ("currency", "selt");
            }

            if (secId.Length > 8 && secId.Any(char.IsDigit))
            {
                return ("stock", "bonds");
            }

            await Task.Delay(1000);
            return ("stock", "shares");
        }

       
    }
}
