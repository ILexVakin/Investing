using Investing.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Investing.Services.Interfaces
{
    public interface ICandlesData<T>
    {
        Task<T> GetCandlesByManyDaysAsync(string endpoint);
        Task<List<CandlesByDayResponse>> GetCandlesByDayAsync(string endpoint);
    }
}
