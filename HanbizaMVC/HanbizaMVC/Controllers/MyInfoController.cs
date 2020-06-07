using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HanbizaMVC.Models;
using System.Linq;

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
        [HttpPost]
        public IActionResult LogIn(OnlyLogin model)
        {
            _logger.LogInformation("LogInProcess: "+model.LoginID +" / "+ model.passW);
            if (ModelState.IsValid)
            {
                using (var db = new HanbizaContext())
                {
                    var user = db.LoginInfor.FirstOrDefault(u => u.LoginId.Equals(model.LoginID) &&
                                                            u.PassW.Equals(model.passW));
                    if(user != null)
                    {
                        //성공
                        return new RedirectResult("/Home/Index");
                    }
                }
                ModelState.AddModelError(string.Empty, "사용자 ID 혹은 비밀번호가 올바르지 않습니다.");
            }
             
            return View(model);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
