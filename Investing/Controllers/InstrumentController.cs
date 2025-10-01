using Investing.Models;
using Investing.Services;
using Investing.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Threading.Tasks;

namespace Investing.Controllers
{
    public class InstrumentController : Controller
    {
        private readonly ISearchExchangeInstrumentsService _searchService;
        public InstrumentController(ISearchExchangeInstrumentsService searchService)
        {
            _searchService = searchService;
        }
        //[HttpGet("instrument/getlistinstruments")]
        public async Task<ActionResult<IEnumerable<SingleModelExchangeInstruments>>> GetListInstruments(string substring)
        {
            if (!string.IsNullOrEmpty(substring))
            {
                return View/*Json*/(await _searchService.SearchAllExchangeInstrumentsBySubstringAsync(substring));
            }
            else
                return BadRequest(new List<SingleModelExchangeInstruments>());
        }
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<TodoItem>>> GetInstrumentBySecIdAsync(string secid)
        //{
        //    return View();
        //}
    }
}
