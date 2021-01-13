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
using System.Net;
using Newtonsoft.Json;

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

        //출퇴근시간 저장
        [Route("/Home/WorkTime_Save/{WorkType}/{NowDate}/{NowTime}")]
        public string WorkTime_Save(string WorkType, string NowDate, string NowTime)
        {
            //Console.WriteLine(WorkType + "/ " + NowDate + " /" + NowTime);
            Boolean checkLogin = CheckLogin();
            IPAddress ip;
            var headers = Request.Headers.ToList();
            if (headers.Exists((kvp) => kvp.Key == "X-Forwarded-For"))
            {
                // when running behind a load balancer you can expect this header
                var header = headers.First((kvp) => kvp.Key == "X-Forwarded-For").Value.ToString();
                ip = IPAddress.Parse(header);
            }
            else
            {
                // this will always have a value (running locally in development won't have the header)
                ip = Request.HttpContext.Connection.RemoteIpAddress;
            }

            int a = _db.LoadStoredProc("WorkTime_insert").AddParam("WorkType", WorkType).AddParam("Dname", LoginUser.Dname).AddParam("BizNum", LoginUser.BizNum)
                      .AddParam("PkDay", NowDate).AddParam("StaffID", LoginUser.StaffId).AddParam("StaffName", LoginUser.StaffName)
                      .AddParam("NowTime", NowDate + " " + NowTime).AddParam("IpAddress", ip.ToString())
                      .ExecNonQuery();
            //Console.WriteLine("a: "+a);
            if (a > 0)
            {
                return "Success";
            }

            return "";
        }

        //출퇴근체크 후 버튼활성화 여부
        [Route("/Home/WorkTime_check/{NowDate}")]
        public IActionResult WorkTime_check(string NowDate)
        {
            Boolean checkLogin = CheckLogin();
            
            List<WorkTime> workTime = null;
            var jsonString = "";

            _db.LoadStoredProc("WorkTime_check").AddParam("PkDay", NowDate)
                .AddParam("Dname", LoginUser.Dname).AddParam("BizNum", LoginUser.BizNum).AddParam("StaffID", LoginUser.StaffId)
                .Exec(r => workTime = r.ToList<WorkTime>());
            jsonString = JsonConvert.SerializeObject(workTime);

            return new JsonResult(jsonString);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
