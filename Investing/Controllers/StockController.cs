using Investing.Data;
using Investing.Models;
using Investing.Models.ViewModels;
using Investing.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Investing.Controllers
{
    public class StockController : Controller
    {
        private readonly MainContext _context;
        private readonly StockMoexApi _moexService;
        public StockController(MainContext context, StockMoexApi moexService)
        {
            _context = context;
            _moexService = moexService;
        }

        [HttpGet]
        public async Task<IActionResult> GetListStocksAsync()
        {
            SearchImageService searchImage = new SearchImageService();
            await searchImage.DownloadImage("SBER");
            var stocks = await _moexService.GetCombinedDataAsync();
            return View(stocks);
        }

        //public async Task<ActionResult> Details(int id)
        //{
        //    CombinedStocsVM combinedStocs = new CombinedStocsVM()
        //    {
        //        Security = combinedStocs.Security.SECID.Where(c=> c.)
        //    }
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<Stock>> GetStockById(long id)
        {
            var stock = await _context.Stock.FindAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return stock;
        }

        public async Task<ActionResult<Stock>> ProcessingFinancialInstrument(long secid) //ISIN(Международный идентификационный номер ценной бумаги)
        {
            var stock = await _context.Stock.FindAsync(secid);

            if (stock == null)
            {
                return NotFound();
            }

            return stock;
        }
    }
}
