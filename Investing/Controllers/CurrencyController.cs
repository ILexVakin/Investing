using Investing.Services.MoexData;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Investing.Controllers
{
    public class CurrencyController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetListCurrency()
        {
            CurrencyData  currencyData = new CurrencyData();
            return View(await currencyData.CombinedCurrencyDataAsync());
        }
    }
}
