using Investing.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Investing.Services.Interfaces
{
    public interface IFullModelInstrumentsMoex
    {
        Task<List<SingleModelExchangeInstruments>> GetStockFullModelAsync();

        Task<List<SingleModelExchangeInstruments>> GetCurrencyFullModelAsync();
        Task<List<SingleModelExchangeInstruments>> GetBondFullModelAsync();
        Task<List<SingleModelExchangeInstruments>> GetFundFullModelAsync();
        Task<List<SingleModelExchangeInstruments>> GetFuturesFullModelAsync();
    }
}
