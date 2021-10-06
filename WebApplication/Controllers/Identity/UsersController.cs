using Core.Constants;
using Core.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication.Models.Users;

namespace WebApplication.Controllers.Identity
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILoggerService _loggerService;
        private readonly IUserHelper _userHelper;

        private const string CONTROLLER_NAME = "users";

        public UsersController(UserManager<ApplicationUser> userManager,
              ILoggerService loggerService,
              IUserHelper userHelper)
        {
            _userManager = userManager;
            _loggerService = loggerService;
            _userHelper = userHelper;
        }

        [HttpGet]
        public IActionResult Index(string searchSelectionString, string searchString)
        {
            var listUsers = _userManager.Users;
            var listViewUsers = new List<UserViewModel>();

            foreach (var listUser in listUsers)
            {
                listViewUsers.Add(
                    new UserViewModel()
                    {
                        Id = listUser.Id,
                        Email = listUser.Email,
                        FLP = $"{listUser.Lastname} {listUser.Firstname} {listUser.Patronymic}"
                    });
            }

            // поиск по списку
            List<string> searchSelection = new List<string>() { "Поиск по", "Идентификатору", "Электронной почте", "Полному имени" };

            searchString = searchString ?? string.Empty;

            // поиск
            if (searchSelectionString != string.Empty && searchSelectionString != null && searchSelectionString != "Поиск" && searchString != null)
            {
                if (searchSelectionString.ToLower() == searchSelection[1].ToLower() && searchString != string.Empty)
                {
                    listViewUsers = listViewUsers.Where(p => p.Id != null && p.Id.ToLower().Contains(searchString.ToLower())).ToList();
                }
                else if (searchSelectionString.ToLower() == searchSelection[1].ToLower() && searchString == string.Empty)
                {
                    listViewUsers = listViewUsers.Where(p => p.Id == null || p.Id == string.Empty).ToList();
                }
                else if (searchSelectionString.ToLower() == searchSelection[2].ToLower() && searchString != string.Empty)
                {
                    listViewUsers = listViewUsers.Where(p => p.Email != null && p.Email.ToLower().Contains(searchString.ToLower())).ToList();
                }
                else if (searchSelectionString.ToLower() == searchSelection[2].ToLower() && searchString == string.Empty)
                {
                    listViewUsers = listViewUsers.Where(p => p.Email == null || p.Email == string.Empty).ToList();
                }
                else if (searchSelectionString.ToLower() == searchSelection[3].ToLower() && searchString != string.Empty)
                {
                    listViewUsers = listViewUsers.Where(p => p.FLP != null && p.FLP.ToLower().Contains(searchString.ToLower())).ToList();
                }
                else if (searchSelectionString.ToLower() == searchSelection[3].ToLower() && searchString == string.Empty)
                {
                    listViewUsers = listViewUsers.Where(p => p.FLP == null || p.FLP == string.Empty || p.FLP == "  ").ToList();
                }
            }

            _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTION_INDEX, LoggerConstants.TYPE_GET, "index", GetCurrentUserId());

            return View(new UserFilterListViewModel()
            {
                ListUsers = new ListUserViewModel { Users = listViewUsers },
                SearchSelection = new SelectList(searchSelection),
                SearchString = searchString,
                SearchSelectionString = searchSelectionString
            });
        }

        [HttpGet]
        public IActionResult Create(string searchSelectionString, string searchString)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;

            _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTION_CREATE, LoggerConstants.TYPE_GET, "create", GetCurrentUserId());

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model, string searchSelectionString, string searchString)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;

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

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTION_CREATE, LoggerConstants.TYPE_POST, $"create user email: {_userHelper.GetIdUserByEmail(model.Email)} successful", GetCurrentUserId());

                    return RedirectToAction("Index", new { searchSelectionString, searchString });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        _loggerService.LogWarning(CONTROLLER_NAME + LoggerConstants.ACTION_CREATE, LoggerConstants.TYPE_POST, $"code:{error.Code}|description:{error.Description}", GetCurrentUserId());

                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id, string searchSelectionString, string searchString)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                _loggerService.LogWarning(CONTROLLER_NAME + LoggerConstants.ACTION_EDIT + $"/{id}", LoggerConstants.TYPE_GET, LoggerConstants.ERROR_USER_NOT_FOUND, GetCurrentUserId());

                return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = "Пользователь не найден" });
            }

            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;

            EditUserViewModel model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Patronymic = user.Patronymic
            };

            _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTION_EDIT, LoggerConstants.TYPE_GET, $"edit user id: {id}", GetCurrentUserId());

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model, string searchSelectionString, string searchString)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;

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
                        _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTION_EDIT, LoggerConstants.TYPE_POST, $"edit  id: {model.Id} successful", GetCurrentUserId());

                        return RedirectToAction("Index", new { searchSelectionString, searchString });
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            _loggerService.LogWarning(CONTROLLER_NAME + LoggerConstants.ACTION_EDIT, LoggerConstants.TYPE_POST, $"code:{error.Code}|description:{error.Description}", GetCurrentUserId());

                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    _loggerService.LogWarning(CONTROLLER_NAME + LoggerConstants.ACTION_EDIT, LoggerConstants.TYPE_POST, LoggerConstants.ERROR_USER_NOT_FOUND, GetCurrentUserId());

                    return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = "Пользователь не найден" });
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id, string searchSelectionString, string searchString)
        {
            if (id == GetCurrentUserId())
                return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = "Вы не можете удалить этого пользователя" });

            ApplicationUser user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTION_DELETE + $"/{id}", LoggerConstants.TYPE_POST, $"delete user id: {id} successful", GetCurrentUserId());

                    return RedirectToAction("Index", new { searchSelectionString, searchString });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        _loggerService.LogWarning(CONTROLLER_NAME + LoggerConstants.ACTION_CREATE, LoggerConstants.TYPE_POST, $"code:{error.Code}|description:{error.Description}", GetCurrentUserId());

                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return RedirectToAction("Error", "Home", new { requestId = "400" });

                }
            }

            _loggerService.LogWarning(CONTROLLER_NAME + LoggerConstants.ACTION_DELETE, LoggerConstants.TYPE_POST, LoggerConstants.ERROR_USER_NOT_FOUND, GetCurrentUserId());

            return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = $"Пользователь не найден: действителен" });
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(string id, string searchSelectionString, string searchString)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = "Пользователь не найден" });
            }

            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;

            ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };

            _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTION_CHANGEPASSWORD + $"/{id}", LoggerConstants.TYPE_GET, $"change password user id: {user.Id}", GetCurrentUserId());

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model, string searchSelectionString, string searchString)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);

                if (user != null)
                {
                    IdentityResult result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                    if (result.Succeeded)
                    {
                        _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTION_CHANGEPASSWORD, LoggerConstants.TYPE_POST, $"change password user id: {user.Id} ", GetCurrentUserId());

                        return RedirectToAction("Index", new { searchSelectionString, searchString });
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            _loggerService.LogWarning(CONTROLLER_NAME + LoggerConstants.ACTION_CHANGEPASSWORD, LoggerConstants.TYPE_POST, $"code:{error.Code}|description:{error.Description}", GetCurrentUserId());

                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    _loggerService.LogWarning(CONTROLLER_NAME + LoggerConstants.ACTION_CHANGEPASSWORD, LoggerConstants.TYPE_POST, LoggerConstants.ERROR_USER_NOT_FOUND, GetCurrentUserId());

                    return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = "Пользователь не найден" });
                }
            }

            return View(model);
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
