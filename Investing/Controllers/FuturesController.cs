using Investing.Services.MoexData;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Investing.Controllers
{
    public class FuturesController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetListFutures()
        {
            return View(await FuturesData.CombinedFuturesDataAsync());
        }
    }
}
