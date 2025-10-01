using Investing.Services.MoexData;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Investing.Controllers
{
    public class FundController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetListFunds()
        {
            return View(await FundData.CombinedFundDataAsync());
        }
    }
}
