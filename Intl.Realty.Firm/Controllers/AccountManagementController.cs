using Intl.Realty.Firm.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Intl.Realty.Firm.Controllers
{
    public class AccountManagementController : Controller
    {
        public IActionResult AccountManagement()
        {
            List<BreadcrumbViewModel> breadcrumbs = new List<BreadcrumbViewModel>();
            breadcrumbs.Add(new BreadcrumbViewModel { DisplayName = "Account Management", Url = Url.Action("AccountManagement", "AccountManagement") ?? "#" });

            if (ViewBag != null)
            {
                ViewBag.Breadcrumbs = breadcrumbs;
            }

            return View();
        }
    }
}
