using Investing.Data;
using Investing.Models;
using Investing.Models.ViewModels;
using Investing.Services;
using Investing.Services.MoexData;
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
        public StockController(MainContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetListStocks()
        {
            SearchImageService searchImage = new SearchImageService();
            await searchImage.DownloadImage("SBER");
            return View(await StockData.CombinedStockDataAsync());
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
