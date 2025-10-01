using Investing.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Investing.Services.Interfaces
{
    public interface ISearchExchangeInstrumentsService
    {
        Task<List<SingleModelExchangeInstruments>> SearchAllExchangeInstrumentsAsync();
        Task<List<SingleModelExchangeInstruments>> SearchAllExchangeInstrumentsBySubstringAsync(string substring);
    }
}
