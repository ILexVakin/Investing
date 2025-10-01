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
            FuturesData futuresData = new FuturesData();    
            return View(await futuresData.CombinedFuturesDataAsync());
        }
    }
}
