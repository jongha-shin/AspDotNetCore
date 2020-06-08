using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HanbizaMVC.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System;

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
                    //var user = db.LoginInfor.FirstOrDefault(u => u.LoginId.Equals(model.LoginID));
                    //&& u.PassW.Equals(model.passW));

                    var user = from loginUser in db.LoginInfor
                               where loginUser.LoginId == model.LoginID
                               select  new { loginUser.BizNum, loginUser.StaffId };
                    
                    if(user != null)
                    {
                        foreach (var item in user)
                        {
                        TempData["StaffId"]= item.StaffId;
                        TempData["BizNum"] = item.BizNum;
                        TempData["DateNow"] = DateTime.Now;
                        }
                        //성공
                        //HttpContext.Session.SetString("loginUser", user.CompanyName);
                        //HttpContext.Session.SetInt32("loginUser", user.StaffId);
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
