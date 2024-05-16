using Intl.Realty.Firm.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Intl.Realty.Firm.Controllers
{
    public class TransactionController : Controller
    {
        public TransactionController()
        {
            
        }
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
