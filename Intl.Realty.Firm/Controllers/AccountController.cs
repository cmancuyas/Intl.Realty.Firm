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

namespace Intl.Realty.Firm.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly Jwt _jwt;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly IReCaptchaService _reCaptchaService;
        private readonly IConfigurationService _configurationService;
        private readonly IFileHandlerService _fileHandlerService;
        private readonly IMemoryCache _memoryCache;
        private int _userId = 1;

        public AccountController(ILogger<AccountController> logger,
                                IOptions<Jwt> jWT,
                                IUnitOfWork unitOfWork,
                                IEmailService emailService,
                                IReCaptchaService reCaptchaService,
                                IConfigurationService configurationService,
                                IFileHandlerService fileHandlerService,
                                IMemoryCache memoryCache)
        {
            _logger = logger;
            _jwt = jWT.Value;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _reCaptchaService = reCaptchaService;
            _configurationService = configurationService;
            _fileHandlerService = fileHandlerService;
            _memoryCache = memoryCache;
        }

        public async Task<IActionResult> SignUp()
        {
            RegisterViewModel viewModel = new RegisterViewModel();

            var employmentStatusIEnum = await _unitOfWork.EmploymentStatus.GetAllAsync();
            viewModel.EmploymentStatusIEnum = SelectListConverter.CreateSelectList(employmentStatusIEnum.ToList(), x => x.Id, x => x.Description);

            var roleIEnum = await _unitOfWork.Role.GetAllAsync();
            viewModel.RoleIEnum = SelectListConverter.CreateSelectList(roleIEnum.ToList(), x => x.Id, x => x.Description);

            var departmentIEnum = await _unitOfWork.Department.GetAllAsync();
            viewModel.DepartmentIEnum = SelectListConverter.CreateSelectList(departmentIEnum.ToList(), x => x.Id, x => x.Description);
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
                    //ModelState.AddModelError(string.Empty, "Invalid reCAPTCHA.");
                    //return Json(new { success = false, elementId = "reCaptchaError", message = "Invalid reCAPTCHA." });
                }
                //end of reCaptcha verification

                var email = await _unitOfWork.User.GetAsync(x => x.Email == viewModel.EmailAddress);
                if (email != null)
                {
                    ModelState.AddModelError(string.Empty, "Email already exists");
                    return Json(new { success = false, elementId = "Email Error", message = "Email already exists" });
                }

                if (ModelState.IsValid)
                {
                    byte[] salt;
                    string hashedPassword = PasswordHelper.HashPassword(viewModel.CreatePassword, out salt);

                    string selectedRoleId = Request.Form["SystemRoleDDL"].ToString();
                    string selectedDepartmentId = Request.Form["DepartmentDDL"].ToString();

                    viewModel.CreatePassword = hashedPassword;
                    viewModel.EmploymentStatusId = 1; // Set 1 = Active during register/signup
                    viewModel.DepartmentId = Convert.ToInt32(selectedDepartmentId);
                    viewModel.RoleId = Convert.ToInt32(selectedRoleId);

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
                var userIdPath = _userId.ToString() + "\\";

                var uploadPath = "wwwroot\\files\\img\\avatar";

                var updatedUploadPath = Path.Combine(uploadPath, userIdPath);

                var (fileNameWithoutExtension, fileExtension, originalFileName) = await _fileHandlerService.UploadFile(file, uploadPath);

                var uniqueFileName = fileNameWithoutExtension + fileExtension;

                var profilePicture = new ProfilePicture
                {
                    FileName = fileNameWithoutExtension,
                    FileExtension = fileExtension,
                    OriginalFileName = originalFileName,
                    Directory = uploadPath,
                    FullPath = uploadPath + uniqueFileName,
                    FileSize = file.Length.ToString(),
                };

                await _unitOfWork.ProfilePicture.AddAsync(profilePicture);

                return Ok("/files/img/avatar" + uniqueFileName);
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
        [HttpPost]
        public async Task<IActionResult> Login(AccountViewModel model)
        {
            try
            {
                model.AccountMode = MODE.SIGNIN;
                var validate = await ValidateLogin(model);
                if (validate.Item2)
                {
                    var permissions = await _unitOfWork.RolePermission.GetRolePermissionsByRoleId(3);
                    if (permissions.Any())
                    {
                        var permissionList = permissions.Select(x => x.Description).ToList();
                        var token = JWTToken.GenerateJwtToken(validate.Item1, _jwt.Key, permissionList);

                        Response.Cookies.Append("JWT", token, new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.Strict,
                            Expires = DateTime.UtcNow.AddHours(1)
                        });
                    }
                    bool isSysAd = ValidateIfSysAd(validate.Item1);
                    Session.Configure(HttpContext.Session);
                    Session.SetInt(SessionKey.UserId, validate.Item1.Id);
                    //Activity.Log(ActivityType.LOGIN, typeof(LoginController), model);

                    return RedirectToAction("Dashboard", "Home", new { isSysAd = isSysAd });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
            }
            return View(model);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            AccountViewModel viewModel = new();
            viewModel.ResetPasswordViewModel = new();
            //Activity.Log(ActivityType.LOGOUT, typeof(AccountController), viewModel);
            Session.Clear();
            return View("Login", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(AccountViewModel model)
        {
            try
            {
                model.AccountMode = MODE.RESET;
                if (ModelState["ResetPasswordViewModel.Username"]!.Errors.Any())
                {
                    ModelState.ClearValidationState(nameof(model.Username));
                    ModelState.ClearValidationState(nameof(model.Password));
                    return View("Login", model);
                }

                ModelState.Clear();
                var user = await _unitOfWork.User.GetAsync(x => x.Email == model.Username);
                if (user is null)
                {
                    ModelState.AddModelError(nameof(model.ResetPasswordViewModel.Username), "Email did not exist");
                }
                else
                {
                    var resetLink = CreateResetPasswordTokenRequest(model.ResetPasswordViewModel);
                    if (!string.IsNullOrEmpty(resetLink))
                    {
                        await CreateResetPasswordEmailRequest(user, resetLink);
                        model.AccountMode = MODE.RESET_REQUEST;
                    }
                    else
                        ModelState.AddModelError("ResetPasswordViewModel.Username", "Reset Password email request already sent");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
            }

            return View("Login", model);

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

            var user = await _unitOfWork.User.GetAsync(x => x.Email == model.Username);
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
        private async Task CreateResetPasswordEmailRequest(User user, string resetLink)
        {
            var template = EmailTemplate.UserResetPasswordTemplate(user, resetLink);
            var mailRequest = new MailRequest
            {
                ToEmail = user.Email,
                Subject = "do not reply",
                Body = template
            };
            await _emailService.SendEmailAsync(mailRequest);
        }

        private string CreateResetPasswordTokenRequest(ResetPasswordViewModel viewModel)
        {
            string callBack = string.Empty;
            var userHash = viewModel.Username!.GetHashCode();
            if (!_memoryCache.TryGetValue(userHash!, out _))
            {
                _memoryCache.Set<string>(userHash, viewModel.Username, TimeSpan.FromMinutes(20));

                callBack = Url.Action("Index", "ResetPassword",
                    new
                    {
                        rt = userHash,
                    }, protocol: Request.Scheme)!;
            }
            return callBack;
        }
        private bool ValidateIfSysAd(User user)
        {
            if (user == null)
                return false;
            if (user.Role == null)
                return false;

            if (user.Role.Code.Contains("SysAd", StringComparison.OrdinalIgnoreCase))
                return true;
            else
                return false;
        }
    }
}
