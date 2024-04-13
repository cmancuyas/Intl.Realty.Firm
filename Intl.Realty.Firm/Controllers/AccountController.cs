using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DENR_FAPIS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Intl.Realty.Firm.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login()
        {
            AccountViewModel model = new AccountViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(AccountViewModel model)
        {
            if (model.Username == model.SetUsername && model.Password == model.SetPassword)
            {
                return RedirectToAction("Dashboard", "Home");
            }
            else if (model.Username == model.SetUsername && model.Password != model.SetPassword)
            {
                ViewBag.Error = "Invalid password.";
            }
            else if (model.Username != model.SetUsername && model.Password == model.SetPassword)
            {
                ViewBag.Error = "Invalid username.";
            }
            else 
            {
                ViewBag.Error = "Invalid username and password.";
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Login", "Account");
        }

        public IActionResult SignUp()
        {
            RegisterViewModel model = new RegisterViewModel();

            model.OfficeNameList = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Office 1" },
                new SelectListItem { Value = "2", Text = "Office 2" },
            };

            model.DivisionNameList = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Division 1" },
                new SelectListItem { Value = "2", Text = "Division 2" },
            };

            model.PositionList = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Position 1" },
                new SelectListItem { Value = "2", Text = "Position 2" },
            };

            model.EmploymentStatusList = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Employment Status 1" },
                new SelectListItem { Value = "2", Text = "Employment Status 2" },
            };

            model.SystemRoleList = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "System Role 1" },
                new SelectListItem { Value = "2", Text = "System Role 2" },
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult SignUp(RegisterViewModel model)
        {
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> UploadAvatar()
        {
            var file = Request.Form.Files[0];
            if (file != null && file.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "img", "avatar");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return Ok("/files/img/avatar/" + uniqueFileName);
            }
            else
            {
                return BadRequest("No file uploaded.");
            }
        }
    }
}
