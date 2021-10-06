﻿using Core.DTO;
using Core.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Models.Catalog;
using Core.Constants;
using System.Security.Claims;
using Core.Exceptions;
using WebApplication.Interfaces;

namespace WebApplication.Controllers
{
    [Authorize(Roles = "admin")]
    public class CatalogController : Controller
    {
        private readonly ICatalogService _сatalogService;
        private readonly IProviderService _providerService;
        private readonly ILoggerService _loggerService;

        private const string CONTROLLER_NAME = "catalog";

        public CatalogController(ICatalogService сatalogService,
            IProviderService providerService,
            ILoggerService loggerService)
        {
            _сatalogService = сatalogService;
            _providerService = providerService;
            _loggerService = loggerService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index(int providerId, int? menuId, string searchSelectionString, string searchString, SortState sortCatalog = SortState.NameAsc)
        {
            IEnumerable<CatalogDTO> сatalogDTOs = _сatalogService.GetСatalogs(providerId);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CatalogDTO, CatalogViewModel>()).CreateMapper();
            var catalogs = mapper.Map<IEnumerable<CatalogDTO>, List<CatalogViewModel>>(сatalogDTOs);

            var provider = _providerService.GetProvider(providerId);

            if (provider == null)
                return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = "Доставщик не найден" });

            ViewData["NameProvider"] = string.Empty + provider.Name;

            // поиск по списку
            List<string> searchSelection = new List<string>() { "Поиск по", "Каталогу", "Информации" };

            searchString = searchString ?? string.Empty;

            // поиск
            if (searchSelectionString != string.Empty && searchSelectionString != null && searchSelectionString != "Поиск" && searchString != null)
            {
                if (searchSelection[1].ToLower() == searchSelectionString.ToLower() && searchString != string.Empty)
                {
                    catalogs = catalogs.Where(p => p.Name != null && p.Name.ToLower().Contains(searchString.ToLower())).ToList();
                }
                else if (searchSelection[1].ToLower() == searchSelectionString.ToLower() && searchString == string.Empty)
                {
                    catalogs = catalogs.Where(p => p.Name == null || p.Name == string.Empty).ToList();
                }
                else if (searchSelection[2].ToLower() == searchSelectionString.ToLower() && searchString != string.Empty)
                {
                    catalogs = catalogs.Where(p => p.Info != null && p.Info.ToLower().Contains(searchString.ToLower())).ToList();
                }
                else if (searchSelection[2].ToLower() == searchSelectionString.ToLower() && searchString == string.Empty)
                {
                    catalogs = catalogs.Where(p => p.Name == null || p.Name == string.Empty).ToList();
                }
            }

            ViewBag.NameSort = sortCatalog == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;

            catalogs = sortCatalog switch
            {
                SortState.NameDesc => catalogs.OrderByDescending(s => s.Name).ToList(),
                _ => catalogs.OrderBy(s => s.Name).ToList(),
            };

            _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTION_INDEX + $"/{providerId}", LoggerConstants.TYPE_GET, $"index – get catalogs of provider id: {providerId}", GetCurrentUserId());

            return View(new CatalogdProviderIdViewModel()
            {
                MenuId = menuId,
                Catalogs = catalogs,
                ProviderId = providerId,
                SearchString = searchString,
                SearchSelection = new SelectList(searchSelection),
                SearchSelectionString = searchSelectionString
            });
        }

        // Для администратора

        [HttpGet]
        public IActionResult Add(int providerId, int? menuId, string searchSelectionString, string searchString, SortState sortCatalog)
        {
            ViewBag.MenuId = menuId;
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;
            ViewBag.NameSort = sortCatalog == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;

            _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTION_ADD + $"/{providerId}", LoggerConstants.TYPE_GET, $"add catalog provider id: {providerId}", GetCurrentUserId());

            return View(new AddCatalogViewModel() { ProviderId = providerId });
        }

        [HttpPost]
        public IActionResult Add(AddCatalogViewModel model, int? menuId, string searchSelectionString, string searchString, SortState sortCatalog)
        {
            ViewBag.MenuId = menuId;
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;
            ViewBag.NameSort = sortCatalog;

            if (ModelState.IsValid)
            {
                CatalogDTO сatalogDTO = new CatalogDTO()
                {
                    Info = model.Info,
                    Name = model.Name,
                    ProviderId = model.ProviderId
                };

                try
                {
                    _сatalogService.AddСatalog(сatalogDTO);

                    _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTION_ADD, LoggerConstants.TYPE_POST, $"add catalog name: {model.Name} provider id: {model.ProviderId} successful", GetCurrentUserId());

                    return RedirectToAction("Index", new { model.ProviderId, menuId, searchSelectionString, searchString, sortCatalog });
                }
                catch (ValidationException ex)
                {
                    _loggerService.LogWarning(CONTROLLER_NAME + LoggerConstants.ACTION_ADD, LoggerConstants.TYPE_POST, $"add catalog name: {model.Name} provider id: {model.ProviderId} error: {ex.Message}", GetCurrentUserId());

                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id, int providerId, string searchSelectionString, string searchString, int? menuId, SortState sortCatalog)
        {
            try
            {
                _сatalogService.DeleteСatalog(id);
            }
            catch (ValidationException ex)
            {
                _loggerService.LogWarning(CONTROLLER_NAME + LoggerConstants.ACTION_DELETE, LoggerConstants.TYPE_POST, $"delete catalog id: {id} provider id: {providerId} error: {ex.Message}", GetCurrentUserId());

                return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = ex.Message });
            }

            _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTION_DELETE, LoggerConstants.TYPE_POST, $"delete catalog id: {id} provider id: {providerId} successful", GetCurrentUserId());

            sortCatalog = sortCatalog == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;

            return RedirectToAction("Index", new { providerId, searchSelectionString, searchString });
        }

        [HttpGet]
        public IActionResult Edit(int id, int? menuId, string searchSelectionString, string searchString, SortState sortCatalog)
        {
            ViewBag.MenuId = menuId;
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;
            ViewBag.NameSort = sortCatalog == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;

            CatalogDTO сatalogDTO = _сatalogService.GetСatalog(id);

            var provider = new EditCatalogViewModel()
            {
                Id = сatalogDTO.Id,
                Info = сatalogDTO.Info,
                Name = сatalogDTO.Name,
                ProviderId = сatalogDTO.ProviderId
            };

            _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTION_EDIT + $"/{id}", LoggerConstants.TYPE_GET, $"edit catalog id: {id} provider id: {provider.ProviderId}", GetCurrentUserId());

            return View(provider);
        }

        [HttpPost]
        public IActionResult Edit(EditCatalogViewModel model, int? menuId, string searchSelectionString, string searchString, SortState sortCatalog)
        {
            ViewBag.MenuId = menuId;
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;
            ViewBag.NameSort = sortCatalog;

            if (ModelState.IsValid)
            {
                CatalogDTO сatalogDTO = new CatalogDTO
                {
                    Id = model.Id,
                    Name = model.Name,
                    Info = model.Info,
                    ProviderId = model.ProviderId
                };

                try
                {
                    _сatalogService.EditСatalog(сatalogDTO);

                    _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTION_EDIT, LoggerConstants.TYPE_POST, $"edit catalog id: {model.Id} provider id: {model.ProviderId} successful", GetCurrentUserId());

                    return RedirectToAction("Index", new { model.ProviderId, menuId, searchSelectionString, searchString, sortCatalog });
                }
                catch (ValidationException ex)
                {
                    _loggerService.LogWarning(CONTROLLER_NAME + LoggerConstants.ACTION_EDIT, LoggerConstants.TYPE_POST, $"edit catalog id: {model.Id} provider id: {model.ProviderId} error: {ex.Message}", GetCurrentUserId());

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
