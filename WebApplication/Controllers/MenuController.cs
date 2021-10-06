using Core.DTO;
using Core.Exceptions;
using Core.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Models.Menu;
using Core.Constants;
using System.Security.Claims;
using WebApplication.Interfaces;

namespace WebApplication.Controllers
{
    [Authorize(Roles = "admin")]
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;
        private readonly IProviderService _providerService;
        private readonly ILoggerService _loggerService;

        private const string CONTROLLER_NAME = "menu";

        public MenuController(IMenuService menuService,
            IProviderService providerService,
            ILoggerService loggerService)
        {
            _menuService = menuService;
            _providerService = providerService;
            _loggerService = loggerService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index(int providerId, string searchSelectionString, string searchString, SortState sortMenu = SortState.DateAsc)
        {
            IEnumerable<MenuDTO> menusDtos = _menuService.GetMenus(providerId);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<MenuDTO, MenuViewModel>()).CreateMapper();
            var menus = mapper.Map<IEnumerable<MenuDTO>, List<MenuViewModel>>(menusDtos);

            var provider = _providerService.GetProvider(providerId);

            if (provider == null)
                return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = "Доставщик не найден" });

            ViewData["NameProvider"] = string.Empty + provider.Name;

            // поиск по списку
            List<string> searchSelection = new List<string>() { "Поиск по", "Информации", "Дате добавления" };

            searchString = searchString ?? string.Empty;

            // поиск
            if (searchSelectionString != string.Empty && searchSelectionString != null && searchSelectionString != "Поиск" && searchString != null)
            {
                if (searchSelectionString.ToLower() == searchSelection[1].ToLower() && searchString != string.Empty)
                {
                    menus = menus.Where(p => p.Info != null && p.Info.ToLower().Contains(searchString.ToLower())).ToList();
                }
                else if (searchSelectionString.ToLower() == searchSelection[1].ToLower() && searchString == string.Empty)
                {
                    menus = menus.Where(p => p.Info == null || p.Info == string.Empty).ToList();
                }
                else if (searchSelectionString.ToLower() == searchSelection[2].ToLower() && searchString != string.Empty)
                {
                    menus = menus.Where(p => p.Date != null && p.Date.ToShortDateString().Contains(searchString.ToLower())).ToList();
                }
                else if (searchSelectionString.ToLower() == searchSelection[2].ToLower() && searchString == string.Empty)
                {
                    menus = menus.Where(p => p.Date == null).ToList();
                }
            }

            ViewBag.DateSort = sortMenu == SortState.DateAsc ? SortState.DateDesc : SortState.DateAsc;

            menus = sortMenu switch
            {
                SortState.DateDesc => menus.OrderByDescending(s => s.Date).ToList(),
                _ => menus.OrderBy(s => s.Date).ToList(),
            };

            _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTION_INDEX + $"/{providerId}", LoggerConstants.TYPE_GET, $"index – get menus of provider id: {providerId}", GetCurrentUserId());

            return View(new MenuAndProviderIdViewModel()
            {
                Menus = menus,
                ProviderId = providerId,
                SearchString = searchString,
                SearchSelection = new SelectList(searchSelection),
                SearchSelectionString = searchSelectionString
            });
        }

        // Для администратора

        [HttpGet]
        public IActionResult Add(int providerId, string searchSelectionString, string searchString, SortState sortMenu)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;
            ViewBag.DateSort = sortMenu == SortState.DateAsc ? SortState.DateDesc : SortState.DateAsc;

            _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTION_ADD + $"/{providerId}", LoggerConstants.TYPE_GET, $"add menu provider id: {providerId}", GetCurrentUserId());

            return View(new AddMenuViewModel() { ProviderId = providerId, Date = DateTime.Now });
        }

        [HttpPost]
        public IActionResult Add(AddMenuViewModel model, string searchSelectionString, string searchString, SortState sortMenu)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;
            ViewBag.DateSort = sortMenu == SortState.DateAsc ? SortState.DateDesc : SortState.DateAsc;

            if (ModelState.IsValid)
            {
                MenuDTO menuDTO = new MenuDTO()
                {
                    Info = model.Info,
                    ProviderId = model.ProviderId,
                    Date = model.Date
                };

                try
                {
                    _menuService.AddMenu(menuDTO);

                    _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTION_ADD, LoggerConstants.TYPE_POST, $"add menu date: {model.Date} provider id: {model.ProviderId} successful", GetCurrentUserId());

                    return RedirectToAction("Index", new { model.ProviderId, searchSelectionString, searchString, sortMenu });
                }
                catch (ValidationException ex)
                {
                    _loggerService.LogWarning(CONTROLLER_NAME + LoggerConstants.ACTION_ADD, LoggerConstants.TYPE_POST, $"add menu date: {model.Date} provider id: {model.ProviderId} error: {ex.Message}", GetCurrentUserId());

                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id, int providerId, string searchSelectionString, string searchString, SortState sortMenu)
        {
            try
            {
                _menuService.DeleteMenu(id);
            }
            catch (ValidationException ex)
            {
                _loggerService.LogWarning(CONTROLLER_NAME + LoggerConstants.ACTION_DELETE, LoggerConstants.TYPE_POST, $"delete menu id: {id} provider id: {providerId} error: {ex.Message}", GetCurrentUserId());

                return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = ex.Message });
            }

            _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTION_DELETE, LoggerConstants.TYPE_POST, $"delete menu id: {id} provider id: {providerId} successful", GetCurrentUserId());

            sortMenu = sortMenu == SortState.DateAsc ? SortState.DateDesc : SortState.DateAsc;

            return RedirectToAction("Index", new { providerId, searchSelectionString, searchString, sortMenu });
        }

        [HttpGet]
        public IActionResult Edit(int id, string searchSelectionString, string searchString, SortState sortMenu)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;
            ViewBag.DateSort = sortMenu == SortState.DateAsc ? SortState.DateDesc : SortState.DateAsc;

            MenuDTO menuDTO = _menuService.GetMenu(id);

            if (menuDTO == null)
                return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = "Меню не найдено" });

            var provider = new EditMenuViewModel()
            {
                Id = menuDTO.Id,
                Date = menuDTO.Date,
                Info = menuDTO.Info,
                ProviderId = menuDTO.ProviderId
            };

            _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTION_EDIT + $"/{id}", LoggerConstants.TYPE_GET, $"edit menu id: {id} provider id: {provider.ProviderId}", GetCurrentUserId());

            return View(provider);
        }

        [HttpPost]
        public IActionResult Edit(EditMenuViewModel model, string searchSelectionString, string searchString, SortState sortMenu)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;
            ViewBag.DateSort = sortMenu;

            if (ModelState.IsValid)
            {
                MenuDTO menuDTO = new MenuDTO
                {
                    Id = model.Id,
                    Date = model.Date,
                    Info = model.Info,
                    ProviderId = model.ProviderId
                };

                try
                {
                    _menuService.EditMenu(menuDTO);

                    _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTION_EDIT, LoggerConstants.TYPE_POST, $"edit menu id: {model.Id} provider id: {model.ProviderId} successful", GetCurrentUserId());

                    return RedirectToAction("Index", new { model.ProviderId, searchSelectionString, searchString, sortMenu });
                }
                catch (ValidationException ex)
                {
                    _loggerService.LogWarning(CONTROLLER_NAME + LoggerConstants.ACTION_EDIT, LoggerConstants.TYPE_POST, $"edit menu id: {model.Id} provider id: {model.ProviderId} error: {ex.Message}", GetCurrentUserId());

                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            }

            return View(model);
        }

        //

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
