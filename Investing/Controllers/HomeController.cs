using Investing.Models;
using Investing.Services;
using Investing.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Investing.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISearchExchangeInstrumentsService _searchService;
        public HomeController(ILogger<HomeController> logger, ISearchExchangeInstrumentsService searchService)
        {
            _logger = logger;
            _searchService = searchService;
        }
        //[HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("api/home/getinstruments")]
        public async Task<JsonResult> GetInstruments()
        {
            var results = await _searchService.SearchAllExchangeInstrumentsAsync();
            return Json(results);
        }
    }
}
