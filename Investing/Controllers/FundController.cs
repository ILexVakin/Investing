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
            FundData fundData = new FundData();
            return View(await fundData.CombinedFundDataAsync());
        }
    }
}
