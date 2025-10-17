using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Investing.Models;

namespace Investing.Services.Interfaces
{
    public interface ICandlesHistory<T>
    {
        Task<List<T>> GetHistoryInstrumentMoreDaysAsync(CandlesRequest model);
        Task<List<CandlesByDayResponse>> GetHistoryInstrumentOneDayAsync(CandlesRequest model);
    }
}
