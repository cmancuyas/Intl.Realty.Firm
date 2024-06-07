using Microsoft.AspNetCore.Mvc;

namespace Intl.Realty.Firm.Controllers
{
    public class LeaseListingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
