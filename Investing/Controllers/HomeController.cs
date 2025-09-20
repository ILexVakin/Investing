using Investing.Models;
using Investing.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public async Task<IActionResult> Search(string substring)
        {
            if (string.IsNullOrEmpty(substring) || substring.Length < 3)
            {
                return BadRequest(new { error = "Минимум 3 символа" });
            }

            var results = await _searchService.SearchAllExchangeInstrumentsAsync(substring);
            return Ok(new { substring, results });
        }
    }
}
