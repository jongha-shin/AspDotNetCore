using HanbizaMVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using StoredProcedureEFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HanbizaMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly HanbizaContext _db;
        public static LoginInfor LoginUser;
        public static List<회사별메뉴> menulist;

        public AccountController(HanbizaContext db)
        {
            _db = db;
        }
        public IActionResult Login()
        {
            // 쿠키 체크, 정보 가져오기
            //Console.WriteLine("로그인 화면");
            if (User.Identity.IsAuthenticated)
            {
                string LoginId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var loginInfo = from info in _db.LoginInfor
                              where info.LoginId == LoginId
                              select info;

                foreach (var item in loginInfo)
                {
                    ViewBag.loginID = item.LoginId;
                    // pwd 저장시 여기서 가져와서 로그인 화면으로
                } 
            }

            return View();

        }
        [Route("/Account/Login/{userID}/{userPWD}/{autoSave}")]
        public string Login(string userID, string userPWD, string autoSave)
        {
            //Console.WriteLine("login() autoSave: "+ autoSave);
            LoginInfor _LoginUser = new LoginInfor();
            _db.LoadStoredProc("dbo.login_Process").AddParam("loginID", userID).AddParam("passW", userPWD)
              .Exec(r => _LoginUser = r.SingleOrDefault<LoginInfor>());
            LoginUser = _LoginUser;
            string rs;
            if (LoginUser != null)
            {
                rs = "success";
                menulist = _db.회사별메뉴.Where(r => r.BizNum == LoginUser.BizNum).ToList();
                var claims = BuildClaims(LoginUser);
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                if (autoSave.Equals("not_save"))
                {
                    //Console.WriteLine("------auto_save NONONONONO------");
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal,
                    new AuthenticationProperties { IsPersistent = false });
                }
                else
                {
                    //Console.WriteLine("------auto_save------");
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal,
                         new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTime.UtcNow.AddDays(50) });
                        //new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTime.UtcNow.AddSeconds(10) });
                }
            }
            else
            {
                rs = "fail";
            }
            return rs;
        }

        private IList<Claim> BuildClaims(LoginInfor account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, $"{account.LoginId}"),
                //new Claim("StaffName", account.StaffName),
                //new Claim("Dname", account.Dname),
                //new Claim("BizNum", account.BizNum),
                //new Claim("StaffID", $"{account.StaffId}"),
                //new Claim("DateNow",  DateTime.Now.ToShortDateString().Substring(0, 7)),
            };
            return claims;
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");

            return RedirectToAction("LogIn", "Account");
        }


        public ActionResult SideMenu()
        {
            return PartialView("SideMenu");
        }
    }
}
