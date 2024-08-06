using Intl.Realty.Firm.Models.Models.Auxiliary;
using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Utility.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Intl.Realty.Firm.Repository.IRepository;
using Intl.Realty.Firm.Service.IServices;
using Intl.Realty.Firm.Utility.Mapper;
using Intl.Realty.Firm.Helper;
using Intl.Realty.Firm.Helper.Auxiliary;
using Intl.Realty.Firm.Models.Models.ViewModel.AccountVM;
using Newtonsoft.Json.Linq;

namespace Intl.Realty.Firm.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IOptions<Jwt> _jWT;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly IReCaptchaService _reCaptchaService;
        private readonly IConfigurationService _configurationService;
        private readonly IMemoryCache _memoryCache;

        public AccountController(ILogger<AccountController> logger, 
                                IOptions<Jwt> jWT, 
                                IUnitOfWork unitOfWork,
                                IEmailService emailService,
                                IReCaptchaService reCaptchaService, 
                                IConfigurationService configurationService,
                                IMemoryCache memoryCache)
        {
            _logger = logger;
            _jWT = jWT;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _reCaptchaService = reCaptchaService;
            _configurationService = configurationService;
            _memoryCache = memoryCache;
        }

        public async Task<IActionResult> SignUp()
        {
            RegisterViewModel viewModel = new RegisterViewModel();

            var employmentStatusList = await _unitOfWork.EmploymentStatus.GetAllAsync();
            viewModel.EmploymentStatusIEnum = SelectListConverter.CreateSelectList(employmentStatusList.ToList(), x => x.Id, x => x.Description);

            var roleList = await _unitOfWork.Role.GetAllAsync();
            viewModel.RoleIEnum = SelectListConverter.CreateSelectList(roleList.ToList(), x => x.Id, x => x.Description);
            //Get reCaptcha Site Key (GetLocalSiteKey for localhost and GetSiteKey if not)
            viewModel.ReCaptchaSiteKey = Request.Host.Value.Contains("localhost") == true ? _configurationService.GetLocalSiteKey() : _configurationService.GetSiteKey();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterViewModel viewModel)
        {
            try
            {
                // Verify reCaptcha
                var reCaptchaToken = Request.Form["g-recaptcha-response"];
                var isReCaptchaValid =
                    Request.Host.Value.Contains("localhost") == true ?
                    await _reCaptchaService.VerifyLocalReCaptcha(reCaptchaToken!) : //verify reCaptcha for localhost
                    await _reCaptchaService.VerifyReCaptcha(reCaptchaToken!); // verify reCaptcha if deployed

                if (!isReCaptchaValid)
                {
                    ModelState.AddModelError(string.Empty, "Invalid reCAPTCHA.");
                    return Json(new { success = false, elementId = "reCaptchaError", message = "Invalid reCAPTCHA." });
                }
                //end of reCaptcha verification

                if (ModelState.IsValid)
                {
                    var user = viewModel.ToUserModel();
                    await _unitOfWork.User.AddAsync(user);

                    // Save Profile Picture to Server and get the model
                    if (viewModel.ProfilePhoto != null)
                    {
                        var profilePictureModel = ProfilePictureHandler.SaveProfilePicture(viewModel.ProfilePhoto, user.Id);

                        // Save the model to database
                        await _unitOfWork.ProfilePicture.AddAsync(profilePictureModel);

                        // Update the CurrentProfilePictureId column
                        user.ProfilePictureId = profilePictureModel.Id;
                    }
                    await _unitOfWork.User.UpdateAsync(user);
                    await CreateEmailRequest(viewModel);
                    return Json(new { success = true });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
            }
            return View(viewModel);
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

        private async Task CreateEmailRequest(RegisterViewModel registerViewModel)
        {
            var template = EmailTemplate.UserRegistrationTemplate(registerViewModel);
            var mailRequest = new MailRequest
            {
                ToEmail = registerViewModel.EmailAddress,
                Subject = "do not reply",
                Body = template
            };
            await _emailService.SendEmailAsync(mailRequest);
        }
        [HttpGet]
        public IActionResult Login()
        {
            AccountViewModel viewModel = new();
            viewModel.ResetPasswordViewModel = new();
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AccountViewModel viewModel)
        {
            try
            {
                viewModel.AccountMode = MODE.SIGNIN;
                var validate = await ValidateLogin(viewModel);
                if (validate.Item2)
                {
                    var permissions = await _unitOfWork.Permission.GetPermissionsByRoleId(1);
                    var permissionStringList = permissions.Select(x=>x.Description).ToList();
                    var token = JWTToken.GenerateJwtToken(validate.Item1, _jWT.ToString()!, permissionStringList!);

                    Response.Cookies.Append("JWT", token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTime.UtcNow.AddHours(1)
                    });

                    Session.Configure(HttpContext.Session);
                    Session.SetInt(SessionKey.UserId, validate.Item1.Id);
                    Activity.Log(ActivityType.LOGIN, typeof(AccountController), viewModel);

                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
            }
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task Logout()
        {

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task Register()
        {

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task ForgotPassword(User model)
        {

        }
        public async Task<bool> ResetPassword(User user, string resetLink)
        {
            return true;
        }
        public async Task<bool> ValidateUserName(string userName)
        {
            return true;
        }
        public async Task<bool> ValidatePassword(string password)
        {
            return true;
        }
        public async Task<string> RequestToken(string token)
        {
            string generatedToken = string.Empty;

            return generatedToken;
        }
        private async Task<(User, bool)> ValidateLogin(AccountViewModel model)
        {
            if (string.IsNullOrEmpty(model.Username))
            {
                ModelState.AddModelError(nameof(model.Username), "Email is required");
                return (new(), false);
            }

            if (string.IsNullOrEmpty(model.Password))
            {
                ModelState.AddModelError(nameof(model.Password), "Password is required");
                return (new(), false);
            }

            var user = await _unitOfWork.User.GetAsync(x=>x.Email == model.Username);
            if (user == null)
            {
                ModelState.AddModelError(nameof(model.Username), "Invalid Email");
                return (new(), false);
            }

            if (!user.IsActive)
            {
                ModelState.AddModelError(nameof(model.Username), "Pending Approval");
                return (new(), false);
            }

            bool isPasswordCorrect = PasswordHelper.VerifyPassword(model.Password, user.Password);
            if (!isPasswordCorrect)
            {
                ModelState.AddModelError(nameof(model.Password), "Invalid Password");
                return (new(), false);
            }
            return (user, true);
        }
    }
}
