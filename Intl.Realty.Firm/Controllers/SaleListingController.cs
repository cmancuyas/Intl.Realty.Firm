using Microsoft.AspNetCore.Mvc;

namespace Intl.Realty.Firm.Controllers
{
    public class SaleListingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
