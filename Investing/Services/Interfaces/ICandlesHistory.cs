using System.Threading.Tasks;
using System;

namespace Investing.Services.Interfaces
{
    public interface ICandlesHistory<T>
    {
        Task<T> GetHistoryInstrumentAsync(string secId, DateTime start, DateTime over);
    }
}
