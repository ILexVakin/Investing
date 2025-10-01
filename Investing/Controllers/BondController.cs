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
            return View(await BondData.CombinedBondDataAsync());
        }
    }
}
