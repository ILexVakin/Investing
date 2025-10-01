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
            var futuresList = await futuresData.GetFuturesAsync();
            return View(futuresList);
        }
    }
}
