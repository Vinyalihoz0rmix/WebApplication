using Core.Constants;
using Core.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication.Models.Roles;

namespace WebApplication.Controllers.Identity
{
    [Authorize(Roles = "admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILoggerService _loggerService;

        private const string CONTROLLER_NAME = "roles";

        public RolesController(RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            ILoggerService loggerService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _loggerService = loggerService;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string userId, string searchSelectionString, string searchString)
        {
            // получить пользователей
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                // получить потерянные роли пользователей
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };

                _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTION_EDIT + $"/{userId}", LoggerConstants.TYPE_GET, $"edit roles user id: {user.Id}", GetCurrentUserId());

                ViewBag.SearchSelectionString = searchSelectionString;
                ViewBag.SearchString = searchString;

                return View(model);
            }

            return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = "Пользователь не найден" });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles, string searchSelectionString, string searchString)
        {
            // получить пользователей
            ApplicationUser user = await _userManager.FindByIdAsync(userId);

            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;

            if (user != null)
            {
                // получить список ролей пользователей
                var userRoles = await _userManager.GetRolesAsync(user);

                // получить список ролей пользователей, которые были добавлены
                var addedRoles = roles.Except(userRoles);

                // получить роли, которые были удалены
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTION_EDIT, LoggerConstants.TYPE_POST, $"edit roles user id: {user.Id} successful", GetCurrentUserId());

                return RedirectToAction("Index", "Users", new { searchSelectionString, searchString });
            }

            _loggerService.LogWarning(CONTROLLER_NAME + LoggerConstants.ACTION_EDIT, LoggerConstants.TYPE_POST, LoggerConstants.ERROR_USER_NOT_FOUND, null);

            return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = "Пользователь не найден" });
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
