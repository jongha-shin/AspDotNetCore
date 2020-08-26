using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HanbizaMVC.Models;
using System;
using StoredProcedureEFCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HanbizaMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HanbizaContext _db;
        private LoginInfor LoginUser;
        private List<회사별메뉴> menulist;

        public HomeController(ILogger<HomeController> logger, HanbizaContext db)
        {
            _logger = logger;
            _db = db;
        }

        public Boolean CheckLogin()
        {
            if (User.Identity.IsAuthenticated)
            {
                LoginUser = new LoginInfor();
                string LoginId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var loginInfo = _db.LoginInfor.Where(r => r.LoginId == LoginId).ToList();
                foreach (var item in loginInfo)
                {
                    LoginUser.BizNum = item.BizNum;
                    LoginUser.Dname = item.Dname;
                    LoginUser.StaffName = item.StaffName;
                    LoginUser.StaffId = item.StaffId;
                }

                menulist = _db.회사별메뉴.Where(r => r.BizNum == LoginUser.BizNum && r.DName == LoginUser.Dname).ToList();
                return true;
            }
            else
            {
                return false;
            }
        }

        // 0. 로그인 후 첫 화면 
        [Authorize]
        public IActionResult Index()
        {
            Boolean checkLogin = CheckLogin();
            if(!checkLogin) return RedirectToAction("Login", "Account");

            //ViewBag.LoginUser = LoginUser;
            //_logger.LogInformation("Index(): " + LoginUser.BizNum + " / " + LoginUser.StaffId);
            //Console.WriteLine("Index");
            //List<공지사항> noticeList = _db.공지사항.Where(r => r.LoginId == LoginUser.StaffId || r.VacId == 0).ToList<공지사항>();
            List<공지사항> noticeList = null;
            _db.LoadStoredProc("dbo.notice_getList").AddParam("StaffId", LoginUser.StaffId).AddParam("BizNum", LoginUser.BizNum)
                .AddParam("Dname", LoginUser.Dname).Exec(r => noticeList = r.ToList<공지사항>());

            ViewBag.menulist = menulist;

            return View(noticeList);
        }

        // 00 비밀번호 변경
        [Authorize]
        public IActionResult Sub00()
        {
            Boolean checkLogin = CheckLogin();
            if (!checkLogin) return RedirectToAction("Login", "Account");

            //_logger.LogInformation("sub9(): ");
            ViewBag.menulist = menulist;
            return View();
        }
        [Route("/Home/Sub00_1/{pwd}")]
        public string Sub00_1(string pwd)
        {
            Boolean checkLogin = CheckLogin();
            string rsString = "";
            //_logger.LogInformation("sub9_1(pwd): " + pwd); // 신청x
            List<LoginInfor> loginInfor = null;
            _db.LoadStoredProc("dbo.PWD_check").AddParam("StaffID", LoginUser.StaffId).AddParam("BizNum", LoginUser.BizNum).AddParam("Dname", LoginUser.Dname)
                .AddParam("checkPWD", pwd).Exec(r => loginInfor = r.ToList<LoginInfor>());

            //Console.WriteLine("Sub9_1 rs: " + loginInfor.Count);

            if (loginInfor.Count > 0)
            {
                rsString = "success";
                return rsString;
            }

            rsString = "fail";
            return rsString;
        }
        [Route("/Home/Sub00_2/{pwd}")]
        public string Sub00_2(string pwd)
        {
            Boolean checkLogin = CheckLogin();
            string rsString;
            //_logger.LogInformation("sub9_2(pwd): " + pwd); // 신청x
            int rs = _db.LoadStoredProc("dbo.PWD_change").AddParam("StaffID", LoginUser.StaffId).AddParam("BizNum", LoginUser.BizNum)
                        .AddParam("Dname", LoginUser.Dname).AddParam("changePWD", pwd.ToLower())
                        .ExecNonQuery();

            //Console.WriteLine("Sub9_2 rs: " + rs);

            if (rs > 0)
            {
                rsString = "success";
                return rsString;
            }

            rsString = "fail";
            return rsString;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
