﻿using Core.Constants;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication.Interfaces;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoggerService _loggerService;

        private const string CONTROLLER_NAME = "home";

        public HomeController(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        public IActionResult Index()
        {
            _loggerService.LogInformation(CONTROLLER_NAME + LoggerConstants.ACTION_INDEX, LoggerConstants.TYPE_GET, "index", GetCurrentUserId());

            return RedirectToAction("ListFavoriteProviders", "Provider");
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            _loggerService.LogInformation(CONTROLLER_NAME + "/privacy", LoggerConstants.TYPE_GET, "privacy", GetCurrentUserId());

            return View();
        }

        [HttpGet]
        public IActionResult AboutDelivery()
        {
            _loggerService.LogInformation(CONTROLLER_NAME + "/aboutdelivery", LoggerConstants.TYPE_GET, "aboutdelivery", GetCurrentUserId());

            return View();
        }

        [HttpGet]
        public IActionResult About()
        {
            _loggerService.LogInformation(CONTROLLER_NAME + "/about", LoggerConstants.TYPE_GET, "about", GetCurrentUserId());

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
        public IActionResult Error(string requestId, string errorInfo)
        {
            _loggerService.LogInformation(CONTROLLER_NAME + $"/error/{requestId}&{errorInfo}", LoggerConstants.TYPE_GET, $"error code: {requestId} info: {errorInfo}", GetCurrentUserId());

            return View(new ErrorViewModel() { RequestId = requestId, ErrorInfo = errorInfo });
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
