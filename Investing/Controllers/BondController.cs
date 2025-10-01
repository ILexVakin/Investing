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
            var bondList = await bondData.CombinedBondDataAsync();
            return View(bondList);
        }
    }
}
