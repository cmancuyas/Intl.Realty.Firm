using Intl.Realty.Firm.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Intl.Realty.Firm.Controllers
{
    public class TransactionController : Controller
    {
        public IActionResult Transaction()
        {
            List<BreadcrumbViewModel> breadcrumbs = new List<BreadcrumbViewModel>();
            breadcrumbs.Add(new BreadcrumbViewModel { DisplayName = "Transaction", Url = Url.Action("Transaction", "Transaction") ?? "#" });

            if (ViewBag != null)
            {
                ViewBag.Breadcrumbs = breadcrumbs;
            }
            return View();
        }
    }
}
