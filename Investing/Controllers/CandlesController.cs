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
        ICandlesHistory<CandlesByDayResponse> _candlesHistory;
        public CandlesController(ICandlesHistory<CandlesByDayResponse> candlesHistory)
        {
            _candlesHistory = candlesHistory;
        }

        //возврат данных для разных по количеству дней
        public async Task<IActionResult> GetHistoryCandlesMoreDays([FromBody] CandlesRequest model)
        {
            var response = await _candlesHistory.GetHistoryInstrumentMoreDaysAsync(model);
            return Ok(response);
        }

        //возврат данных для одного дня
        public async Task<IActionResult> GetHistoryCandlesByDay([FromBody] CandlesRequest model)
        {
            var response = await _candlesHistory.GetHistoryInstrumentOneDayAsync(model);
            return Ok(response);
        }
    }
}
