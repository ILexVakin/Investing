using Investing.Models.ViewModels;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Investing.Services.Interfaces
{
    public interface IReadingMoexData
    {
        Task<JsonElement> GetAllRowsByExchange(string url);
        //Task<List<CombinedCurrencyVM>> GetCurrencyAsync();
        //Task<List<BondItemVM>> GetBondsAsync();
        //Task<List<CombinedStocsVM>> GetFundsAsync();
        //Task<List<CombinedStocsVM>> GetFuturesAsync();

    }
}
