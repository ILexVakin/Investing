using DevTrends.MvcDonutCaching;
using Investing.Data;
using Investing.Models;
using Investing.Models.ViewModels;
using Investing.Services;
using Investing.Services.Interfaces;
using Investing.Services.MoexData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Investing.Controllers
{
    public class StockController : Controller
    {
        private readonly MainContext _context;
        IDetailInstrument<Stocks> _detailInstrument;
        public StockController(MainContext context, IDetailInstrument<Stocks> detailInstrument)
        {
            _context = context;
            _detailInstrument = detailInstrument;
        }

        [HttpGet]
        public async Task<IActionResult> GetListStocks()
        {
            StockData stockData = new StockData();
            return View(await stockData.CombinedStockDataAsync());
        }

        public async Task<ActionResult> DetailStock(string secId)
        {
            var stock = await _detailInstrument.DetailInstrument(secId, FullModelInstrumentsMoex.TypeInstrument.Stock);
            return View(stock);
        }

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
