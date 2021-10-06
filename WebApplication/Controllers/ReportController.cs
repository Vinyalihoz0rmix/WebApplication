﻿using Core.Constants;
using Core.Exceptions;
using Core.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using WebApplication.Interfaces;
using WebApplication.Reports;

namespace WebApplication.Controllers
{
    [Authorize(Roles = "admin")]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        private readonly IProviderService _providerService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILoggerService _loggerService;

        private const string CONTROLLER_NAME = "report";

        public ReportController(IReportService reportService,
            IWebHostEnvironment webHostEnvironment,
            IProviderService providerService,
            UserManager<ApplicationUser> userManager,
            ILoggerService loggerService)
        {
            _userManager = userManager;
            _reportService = reportService;
            _webHostEnvironment = webHostEnvironment;
            _providerService = providerService;
            _loggerService = loggerService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTION_INDEX, LoggerConstants.TYPE_GET, "index", GetCurrentUserId());

            return View();
        }

        [HttpPost]
        public IActionResult GetReportProvider(int providerId, DateTime? dateWith = null, DateTime? dateTo = null)
        {
            try
            {
                var provider = _providerService.GetProvider(providerId);

                if (provider == null)
                {
                    return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = "Доставщик не найден" });
                }

                List<List<string>> reportList;
                string title = string.Empty;

                if (dateWith != null && dateTo == null)
                {
                    reportList = _reportService.GetReportProvider(providerId, dateWith.Value);
                    title = $"{ReportConstants.REPORT_BY_PROVIDER} ({provider.Name}) {ReportConstants.PER} {dateWith.Value.ToString("dd.MM.yyyy")}";

                    _loggerService.LogInformation(CONTROLLER_NAME + $"/getreportprovider/{providerId}", LoggerConstants.TYPE_GET, $"get report provider id: {providerId} dateWith: {dateWith}", GetCurrentUserId());
                }
                else if (dateWith != null && dateTo != null)
                {
                    reportList = _reportService.GetReportProvider(providerId, dateWith.Value, dateTo.Value);
                    title = $"{ReportConstants.REPORT_BY_PROVIDER} ({provider.Name}) {ReportConstants.WITH} {dateWith.Value.ToString("dd.MM.yyyy")} \n {ReportConstants.BY} {dateTo.Value.ToString("dd.MM.yyyy")}";

                    _loggerService.LogInformation(CONTROLLER_NAME + $"/getreportprovider/{providerId}", LoggerConstants.TYPE_GET, $"get report provider id: {providerId} dateWith: {dateWith} dateTo: {dateTo}", GetCurrentUserId());
                }
                else
                {
                    reportList = _reportService.GetReportProvider(providerId);
                    title = $"{ReportConstants.REPORT_BY_PROVIDER} ({provider.Name}) {ReportConstants.FOR_ALL_TIME} ";

                    _loggerService.LogInformation(CONTROLLER_NAME + $"/getreportprovider/{providerId}", LoggerConstants.TYPE_GET, $"get report provider id: {providerId}", GetCurrentUserId());
                }

                ReportPDF reportProvider = new ReportPDF(_webHostEnvironment);

                return File(reportProvider.Report(reportList, title), "application/pdf");

            }
            catch (ValidationException ex)
            {
                _loggerService.LogWarning(CONTROLLER_NAME + $"/getreportprovider/{providerId}", LoggerConstants.TYPE_GET, $"get report provider id: {providerId} error: {ex.Message}", GetCurrentUserId());

                return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult GetReportProviders(DateTime? dateWith = null, DateTime? dateTo = null)
        {
            try
            {
                List<List<string>> reportProvidersDTOs;
                string title = string.Empty;

                if (dateWith != null && dateTo == null)
                {
                    reportProvidersDTOs = _reportService.GetReportProviders(dateWith.Value);
                    title = $"{ReportConstants.REPORT_BY_PROVIDERS_PER} {dateWith.Value.ToString("dd.MM.yyyy")}";

                    _loggerService.LogInformation(CONTROLLER_NAME + $"/getreportproviders", LoggerConstants.TYPE_GET, $"get report providers dateWith: {dateWith}", GetCurrentUserId());
                }
                else if (dateWith != null && dateTo != null)
                {
                    reportProvidersDTOs = _reportService.GetReportProviders(dateWith.Value, dateTo.Value);
                    title = $"{ReportConstants.REPORT_BY_PROVIDERS_WITH} {dateWith.Value.ToString("dd.MM.yyyy")} \n {ReportConstants.BY} {dateTo.Value.ToString("dd.MM.yyyy")}";

                    _loggerService.LogInformation(CONTROLLER_NAME + $"/getreportproviders", LoggerConstants.TYPE_GET, $"get report providers dateWith: {dateWith} dateTo: {dateTo}", GetCurrentUserId());
                }
                else
                {
                    reportProvidersDTOs = _reportService.GetReportProviders();
                    title = ReportConstants.REPORT_BY_PROVIDERS_FOR_ALL_TIME;

                    _loggerService.LogInformation(CONTROLLER_NAME + $"/getreportproviders", LoggerConstants.TYPE_GET, $"get report providers", GetCurrentUserId());
                }

                ReportPDF reportProviders = new ReportPDF(_webHostEnvironment);

                return File(reportProviders.Report(reportProvidersDTOs, title), "application/pdf");

            }
            catch (ValidationException ex)
            {
                _loggerService.LogWarning(CONTROLLER_NAME + $"/getreportproviders", LoggerConstants.TYPE_GET, $"get report providers error: {ex.Message}", GetCurrentUserId());

                return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult GetReportUser(string userId, DateTime? dateWith = null, DateTime? dateTo = null)
        {
            try
            {
                var user = _userManager.Users.Where(p => p.Id == userId).FirstOrDefault();

                if (user == null)
                {
                    return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = "Пользователь не найден" });
                }

                List<List<string>> reportUserDTOs = null;
                string title = string.Empty;

                if (dateWith != null && dateTo == null)
                {
                    reportUserDTOs = _reportService.GetReportUser(userId, dateWith.Value);
                    title = $"{ReportConstants.REPORT_BY_USER} ({user.Email}) {ReportConstants.PER} {dateWith.Value.ToString("dd.MM.yyyy")}";

                    _loggerService.LogInformation(CONTROLLER_NAME + $"/getreportuser/{userId}", LoggerConstants.TYPE_GET, $"get report user id: {userId} dateWith: {dateWith}", GetCurrentUserId());
                }
                else if (dateWith != null && dateTo != null)
                {
                    reportUserDTOs = _reportService.GetReportUser(userId, dateWith.Value, dateTo.Value);
                    title = $"{ReportConstants.REPORT_BY_USER} ({user.Email}) {ReportConstants.WITH} {dateWith.Value.ToString("dd.MM.yyyy")} \n {ReportConstants.BY} {dateTo.Value.ToString("dd.MM.yyyy")}";

                    _loggerService.LogInformation(CONTROLLER_NAME + $"/getreportuser/{userId}", LoggerConstants.TYPE_GET, $"get report user id: {userId} dateWith: {dateWith} dateTo: {dateTo}", GetCurrentUserId());
                }
                else
                {
                    reportUserDTOs = _reportService.GetReportUser(userId);
                    title = $"{ReportConstants.REPORT_BY_USER} ({user.Id}) {ReportConstants.FOR_ALL_TIME}";

                    _loggerService.LogInformation(CONTROLLER_NAME + $"/getreportuser/{userId}", LoggerConstants.TYPE_GET, $"get report user id: {userId}", GetCurrentUserId());
                }

                ReportPDF reportUser = new ReportPDF(_webHostEnvironment);

                return File(reportUser.Report(reportUserDTOs, title), "application/pdf");
            }
            catch (ValidationException ex)
            {
                _loggerService.LogWarning(CONTROLLER_NAME + $"/getreportuser/{userId}", LoggerConstants.TYPE_GET, $"get report user id: {userId} error: {ex.Message}", GetCurrentUserId());

                return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult GetReportUsers(DateTime? dateWith = null, DateTime? dateTo = null)
        {
            try
            {
                List<List<string>> reportUsersDTOs = null;
                string title = string.Empty;

                if (dateWith != null && dateTo == null)
                {
                    reportUsersDTOs = _reportService.GetReportUsers(dateWith.Value);
                    title = $"{ReportConstants.REPORT_BY_USERS_PER} {dateWith.Value.ToString("dd.MM.yyyy")}";

                    _loggerService.LogInformation(CONTROLLER_NAME + $"/getreportusers", LoggerConstants.TYPE_GET, $"get report users dateWith: {dateWith}", GetCurrentUserId());
                }
                else if (dateWith != null && dateTo != null)
                {
                    reportUsersDTOs = _reportService.GetReportUsers(dateWith.Value, dateTo.Value);
                    title = $"{ReportConstants.REPORT_BY_USERS_WITH} {dateWith.Value.ToString("dd.MM.yyyy")} \n {ReportConstants.BY} {dateTo.Value.ToString("dd.MM.yyyy")}";

                    _loggerService.LogInformation(CONTROLLER_NAME + $"/getreportusers", LoggerConstants.TYPE_GET, $"get report users dateWith: {dateWith} dateTo: {dateTo}", GetCurrentUserId());
                }
                else
                {
                    reportUsersDTOs = _reportService.GetReportUsers();
                    title = ReportConstants.REPORT_BY_USERS_FOR_ALL_TIME;

                    _loggerService.LogInformation(CONTROLLER_NAME + $"/getreportusers", LoggerConstants.TYPE_GET, $"get report users", GetCurrentUserId());
                }

                ReportPDF reportUsers = new ReportPDF(_webHostEnvironment);

                return File(reportUsers.Report(reportUsersDTOs, title), "application/pdf");
            }
            catch (ValidationException ex)
            {
                _loggerService.LogWarning(CONTROLLER_NAME + $"/getreportusers", LoggerConstants.TYPE_GET, $"get report users error: {ex.Message}", GetCurrentUserId());

                return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = ex.Message });
            }
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
