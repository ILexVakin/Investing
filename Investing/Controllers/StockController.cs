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
        private readonly ReadingMoexData _moexService;
        public StockController(MainContext context, ReadingMoexData moexService)
        {
            _context = context;
            _moexService = moexService;
        }

        [HttpGet]
        public async Task<IActionResult> GetListStocks()
        {
            StockData stockData = new StockData();
            SearchImageService searchImage = new SearchImageService();
            await searchImage.DownloadImage("SBER");
            var stocks = await stockData.CombinedStockDataAsync();
            return View(stocks);
        }

        [HttpGet]
        public async Task<IActionResult> GetListCurrency()
        {
            CurrencyData currencyData = new CurrencyData(); 
            var combined = await currencyData.CombinedCurrencyDataAsync();
            //var result = combined.Select(x => new CurrencyItemVM
            //{
            //    Security = new CurrencySecurity { SecId = x.Security.SECID, SHORTNAME = x.Security.SHORTNAME, PREVPRICE = x.Security.PREVPRICE ?? null, BOARDID = x.Security.BOARDID },
            //    Marketdata = new CurrencyMarketdata { SECID = x.Marketdata?.SECID, BOARDID = x.Marketdata?.BOARDID, MarketPrice2 = x.Marketdata?.MARKETPRICE2, Last = x.Marketdata?.LAST, TRADINGSTATUS = x.Marketdata?.TRADINGSTATUS }
            //}).ToList();
            return View(combined);
        }

        [HttpGet]
        public async Task<IActionResult> GetListBonds()
        {
            var combined = await _moexService.GetBondsAsync();
            var result = combined.Select(x => new BondItemVM
            {
                Security = new BondSecurity { SECID = x.Security.SECID, SHORTNAME = x.Security.SHORTNAME, PREVPRICE = x.Security.PREVPRICE ?? null, BOARDID = x.Security.BOARDID },
                Marketdata = new BondMarketdata { SECID = x.Marketdata?.SECID, BOARDID = x.Marketdata?.BOARDID, MARKETPRICE2 = x.Marketdata?.MARKETPRICE2, LAST = x.Marketdata?.LAST, TRADINGSTATUS = x.Marketdata?.TRADINGSTATUS }
            }).ToList();
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetListFunds()
        {
            var combined = await _moexService.GetFundsAsync();
            var result = combined.Select(x => new FundItemVM
            {
                Security = new FundSecurity { SECID = x.Security.SECID, SHORTNAME = x.Security.SHORTNAME, PREVPRICE = x.Security.PREVPRICE ?? null, BOARDID = x.Security.BOARDID },
                Marketdata = new FundMarketdata { SECID = x.Marketdata?.SECID, BOARDID = x.Marketdata?.BOARDID, MARKETPRICE2 = x.Marketdata?.MARKETPRICE2, LAST = x.Marketdata?.LAST, TRADINGSTATUS = x.Marketdata?.TRADINGSTATUS }
            }).ToList();
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetListFutures()
        {
            var combined = await _moexService.GetFuturesAsync();
            var result = combined.Select(x => new FutureItemVM
            {
                Security = new FutureSecurity { SECID = x.Security.SECID, SHORTNAME = x.Security.SHORTNAME, BOARDID = x.Security.BOARDID },
                Marketdata = new FutureMarketdata { SECID = x.Marketdata?.SECID, BOARDID = x.Marketdata?.BOARDID, LAST = x.Marketdata?.LAST, TRADINGSTATUS = x.Marketdata?.TRADINGSTATUS }
            }).ToList();
            return View(result);
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
