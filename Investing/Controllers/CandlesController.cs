using Investing.Models;
using Investing.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Investing.Controllers
{
    public class CandlesController : Controller
    {
        ICandlesHistory<Stocks> _candlesHistory;
        public CandlesController(ICandlesHistory<Stocks> candlesHistory)
        {
            _candlesHistory = candlesHistory;
        }
        public async Task<IActionResult> GetHistory(Stocks model, DateTime start, DateTime over)
        {
            var response = await _candlesHistory.GetHistoryInstrumentAsync(model.Security.SECID, start, over);
            return Ok(response);
        }
    }
}
