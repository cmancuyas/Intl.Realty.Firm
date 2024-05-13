
using Intl.Realty.Firm.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Intl.Realty.Firm.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Dashboard()
        {
            List<BreadcrumbViewModel> breadcrumbs = new List<BreadcrumbViewModel>();
            breadcrumbs.Add(new BreadcrumbViewModel { DisplayName = "Dashboard", Url = Url.Action("Dashboard", "Home") ?? "#" });

            ViewBag.Breadcrumbs = breadcrumbs!;

            if (ViewBag != null)
            {
                ViewBag.Breadcrumbs = breadcrumbs;
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
