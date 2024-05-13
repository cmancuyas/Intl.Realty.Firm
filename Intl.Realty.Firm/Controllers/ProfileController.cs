using Intl.Realty.Firm.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Intl.Realty.Firm.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Profile()
        {
            List<BreadcrumbViewModel> breadcrumbs = new List<BreadcrumbViewModel>();
            breadcrumbs.Add(new BreadcrumbViewModel { DisplayName = "Profile", Url = Url.Action("Profile", "Profile") ?? "#" });

            if (ViewBag != null)
            {
                ViewBag.Breadcrumbs = breadcrumbs;
            }

            return View();
        }
    }
}
