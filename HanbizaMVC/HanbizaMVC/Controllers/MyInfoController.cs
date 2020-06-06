using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HanbizaMVC.Models;

namespace HanbizaMVC.Controllers
{
    public class MyInfoController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public MyInfoController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult Sub7()
        {
            return View();
        }
        public IActionResult Sub8()
        {
            return View();
        }
        public IActionResult Sub9()
        {
            return View();
        }
        public IActionResult LogIn()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
