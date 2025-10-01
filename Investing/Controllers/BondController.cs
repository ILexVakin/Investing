using Investing.Services.MoexData;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Investing.Controllers
{
    public class BondController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetListBonds()
        {
            BondData bondData = new BondData();
            return View(await bondData.CombinedBondDataAsync());
        }
    }
}
