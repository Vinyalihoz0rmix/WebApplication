using Core.Constants;
using Core.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication.Models.Account;

namespace WebApplication.Controllers.Identity
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly ILoggerService _loggerService;
        private readonly IUserHelper _userHelper;

        private const string CONTROLLER_NAME = "account";

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IWebHostEnvironment appEnvironment,
            ILoggerService loggerService,
            IUserHelper userHelper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appEnvironment = appEnvironment;
            _loggerService = loggerService;
            _userHelper = userHelper;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTIN_REGISTER, LoggerConstants.TYPE_GET, "registeration", null);

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.Email,
                    Firstname = model.Firstname,
                    Lastname = model.Lastname,
                    Patronymic = model.Patronymic
                };

                // добавить пользователя
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // установка файлов cookie
                    await _signInManager.SignInAsync(user, false);

                    _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTIN_REGISTER, LoggerConstants.TYPE_POST, "registration successful", _userHelper.GetIdUserByEmail(model.Email));

                    return RedirectToAction("RegistrationSuccessful");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        _loggerService.LogWarning(CONTROLLER_NAME + LoggerConstants.ACTIN_REGISTER, LoggerConstants.TYPE_POST, $"code:{error.Code}|description:{error.Description}", null);

                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            else
            {
                _loggerService.LogWarning(CONTROLLER_NAME + LoggerConstants.ACTIN_REGISTER, LoggerConstants.TYPE_POST, "wrong valid register", null);
            }

            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTIN_LOGIN, LoggerConstants.TYPE_GET, "login", null);

            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = null;

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    user = _userManager.Users.FirstOrDefault(p => p.Email == model.Email);

                    if (user == null)
                    {
                        _loggerService.LogWarning(CONTROLLER_NAME + LoggerConstants.ACTIN_LOGIN, LoggerConstants.TYPE_POST, LoggerConstants.ERROR_USER_NOT_FOUND, null);

                        return RedirectToAction("Error", "Home", new { requestId = "400" });
                    }

                    _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTIN_LOGIN, LoggerConstants.TYPE_POST, "login successful", user.Id);

                    // проверьте, принадлежит ли URL-адрес приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    _loggerService.LogWarning(CONTROLLER_NAME + LoggerConstants.ACTIN_LOGIN, LoggerConstants.TYPE_POST, "wrong login or password", null);

                    ModelState.AddModelError(string.Empty, "Неверный логин или пароль");
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                ApplicationUser user = _userHelper.GetUserById(GetCurrentUserId());

                if (user == null)
                {
                    _loggerService.LogWarning(CONTROLLER_NAME + LoggerConstants.ACTIN_LOGOUT, LoggerConstants.TYPE_POST, LoggerConstants.ERROR_USER_NOT_FOUND, null);

                    return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = "Пользователь не найден" });
                }

                _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTIN_LOGOUT, LoggerConstants.TYPE_POST, "logout", user.Id);

                // удалить файлы cookie для аутентификации
                await _signInManager.SignOutAsync();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                _loggerService.LogWarning(CONTROLLER_NAME + LoggerConstants.ACTIN_LOGOUT, LoggerConstants.TYPE_GET, LoggerConstants.ERROR_USER_NOT_AUTHENTICATED, null);

                return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = "Пользователь не найден" });
            }
        }

        [HttpGet]
        public IActionResult Profile()
        {
            if (User.Identity.IsAuthenticated)
            {
                ApplicationUser user = _userHelper.GetUserById(GetCurrentUserId());

                if (user == null)
                {
                    _loggerService.LogWarning(CONTROLLER_NAME + LoggerConstants.ACTIN_PROFILE, LoggerConstants.TYPE_GET, LoggerConstants.ERROR_USER_NOT_FOUND, null);

                    return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = "Пользователь не найден" });
                }

                ProfileVIewModel userView = new ProfileVIewModel()
                {
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Patronymic = user.Patronymic,
                    Email = user.Email
                };

                _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTIN_PROFILE, LoggerConstants.TYPE_GET, "profile", user.Id);

                return View(userView);
            }
            else
            {
                _loggerService.LogWarning(CONTROLLER_NAME + LoggerConstants.ACTIN_PROFILE, LoggerConstants.TYPE_GET, LoggerConstants.ERROR_USER_NOT_AUTHENTICATED, null);

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult Edit()
        {
            if (User.Identity.IsAuthenticated)
            {
                ApplicationUser user = _userHelper.GetUserById(GetCurrentUserId());

                if (user == null)
                {
                    _loggerService.LogWarning(CONTROLLER_NAME + LoggerConstants.ACTION_EDIT, LoggerConstants.TYPE_GET, LoggerConstants.ERROR_USER_NOT_FOUND, null);

                    return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = "Пользователь не найден" });
                }

                ProfileVIewModel userView = new ProfileVIewModel()
                {
                    Id = user.Id,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Patronymic = user.Patronymic,
                    Email = user.Email
                };

                _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTION_EDIT, LoggerConstants.TYPE_GET, "edit", user.Id);

                return View(userView);
            }
            else
            {
                _loggerService.LogWarning(CONTROLLER_NAME + LoggerConstants.ACTION_EDIT, LoggerConstants.TYPE_GET, LoggerConstants.ERROR_USER_NOT_AUTHENTICATED, null);

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProfileVIewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser user = await _userManager.FindByIdAsync(model.Id);

                    if (user != null)
                    {
                        user.Email = model.Email;
                        user.UserName = model.Email;
                        user.Firstname = model.Firstname;
                        user.Lastname = model.Lastname;
                        user.Patronymic = model.Patronymic;

                        var result = await _userManager.UpdateAsync(user);

                        if (result.Succeeded)
                        {
                            _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTION_EDIT, LoggerConstants.TYPE_POST, "edit profile successful", user.Id);

                            return RedirectToAction("Profile");
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                _loggerService.LogWarning(CONTROLLER_NAME + LoggerConstants.ACTION_EDIT, LoggerConstants.TYPE_POST, $"code:{error.Code}|description:{error.Description}", user.Id);

                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                        }
                    }
                    else
                    {
                        _loggerService.LogWarning(CONTROLLER_NAME + LoggerConstants.ACTION_EDIT, LoggerConstants.TYPE_POST, LoggerConstants.ERROR_USER_NOT_FOUND, null);

                        return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = "Пользователь не найден" });
                    }
                }

                return View(model);
            }
            else
            {
                _loggerService.LogWarning(CONTROLLER_NAME + LoggerConstants.ACTION_EDIT, LoggerConstants.TYPE_POST, LoggerConstants.ERROR_USER_NOT_AUTHENTICATED, null);

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            if (User.Identity.IsAuthenticated)
            {
                ApplicationUser user = _userHelper.GetUserById(GetCurrentUserId());

                if (user == null)
                {
                    _loggerService.LogWarning(CONTROLLER_NAME + LoggerConstants.ACTION_CHANGEPASSWORD, LoggerConstants.TYPE_GET, LoggerConstants.ERROR_USER_NOT_FOUND, null);

                    return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = "Пользователь не найден" });
                }

                ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };

                _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTION_CHANGEPASSWORD, LoggerConstants.TYPE_GET, "change password", user.Id);

                return View(model);
            }
            else
            {
                _loggerService.LogWarning(CONTROLLER_NAME + LoggerConstants.ACTION_CHANGEPASSWORD, LoggerConstants.TYPE_GET, LoggerConstants.ERROR_USER_NOT_AUTHENTICATED, null);

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(model.Id);

                if (user != null)
                {
                    IdentityResult result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                    if (result.Succeeded)
                    {
                        _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTION_CHANGEPASSWORD, LoggerConstants.TYPE_POST, "change password successful", user.Id);

                        return RedirectToAction("Profile");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            _loggerService.LogWarning(CONTROLLER_NAME + LoggerConstants.ACTION_CHANGEPASSWORD, LoggerConstants.TYPE_POST, $"code:{error.Code}|description:{error.Description}", user.Id);

                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    _loggerService.LogWarning(CONTROLLER_NAME + LoggerConstants.ACTION_CHANGEPASSWORD, LoggerConstants.TYPE_POST, LoggerConstants.ERROR_USER_NOT_FOUND, null);

                    ModelState.AddModelError(string.Empty, "No such user found");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult RegistrationSuccessful()
        {
            ApplicationUser user = _userHelper.GetUserById(GetCurrentUserId());

            if (user == null)
            {
                _loggerService.LogWarning(CONTROLLER_NAME + LoggerConstants.ACTION_REGISTRATIONSUCCESSFUL, LoggerConstants.TYPE_GET, LoggerConstants.ERROR_USER_NOT_FOUND, user.Id);

                return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = "Пользователь не найден" });
            }

            _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTION_REGISTRATIONSUCCESSFUL, LoggerConstants.TYPE_GET, "registration successful", user.Id);

            ViewBag.Name = user.UserName;

            return View();
        }

        private string GetCurrentUserId()
        {
            if (User.Identity.IsAuthenticated)
            {
                return User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            else
            {
                return null;
            }
        }
    }
}