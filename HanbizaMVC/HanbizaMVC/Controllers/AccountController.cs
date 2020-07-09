﻿using HanbizaMVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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

        public AccountController(HanbizaContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Login(OnlyLogin model)
        {
            Console.WriteLine("accoutn login 실행 / model.auto: " + model.Auto_save);
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            // db에서 비교해서 로그인 정보 가져오기
            LoginInfor account = null;

            _db.LoadStoredProc("dbo.login_Process").AddParam("loginID", model.LoginID).AddParam("passW", model.passW)
              .Exec(r => account = r.SingleOrDefault<LoginInfor>());

            if (account != null)
            {
                var claims = BuildClaims(account);
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                if (model.Auto_save == null || model.Auto_save.Equals(""))
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal,
                        new AuthenticationProperties { IsPersistent = false });
                }
                else
                {
                   Console.WriteLine("------auto_save------");
                   await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal,
                        new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTime.UtcNow.AddDays(30) });
                }

                var menulist = _db.회사별메뉴.Where(r => r.BizNum == account.BizNum).ToList();
                Console.WriteLine("1: " + menulist[0].비밀번호변경);
                ViewBag.menulist = menulist;
                return RedirectToAction("Sub0", "Home");

            }
            return View(model);
        }
        private IList<Claim> BuildClaims(LoginInfor account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, $"{account.StaffId}"),
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

            return RedirectToAction("startLogIn", "MyInfo");
        }

        /*  로그인 정보 확인
        public IActionResult LoginPartial()
        {
            string result = "";
            if (User.Identity.IsAuthenticated)
            {
                result = $"<h3>{User.Identity.Name}</h3>";
                foreach (var claim in User.Claims)
                {
                    result += $"{claim.Type} - {claim.Value}<br />";
                }
            }
            else
            {
                result = "로그인하지 않았습니다.";
            }
            return Content(result, "text/html", Encoding.Default);
        }
        */

    }
}
