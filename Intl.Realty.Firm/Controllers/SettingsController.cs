using Intl.Realty.Firm.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Intl.Realty.Firm.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult Settings()
        {
            List<BreadcrumbViewModel> breadcrumbs = new List<BreadcrumbViewModel>();
            breadcrumbs.Add(new BreadcrumbViewModel { DisplayName = "Settings", Url = Url.Action("Settings", "Settings") ?? "#" });

            if (ViewBag != null)
            {
                ViewBag.Breadcrumbs = breadcrumbs;
            }

            return View();
        }
    }
}
