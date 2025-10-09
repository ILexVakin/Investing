using Investing.Models;
using Investing.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Investing.Services
{
    public class CandlesHistory<T> : ICandlesHistory<T> where T : class, new()
    {
        async Task<T> ICandlesHistory<T>.GetHistoryInstrumentAsync(string secId, DateTime start, DateTime over)
        {
           var path = EndPointsMoex.StockСandlesInfo;
           return (T)(object)path;
        }
    }
}
