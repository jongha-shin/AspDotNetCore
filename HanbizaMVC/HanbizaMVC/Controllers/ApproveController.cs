using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HanbizaMVC.Models;
using System;
using StoredProcedureEFCore;
using System.Collections.Generic;
using HanbizaMVC.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Newtonsoft.Json;

namespace HanbizaMVC.Controllers
{
    public class ApproveController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HanbizaContext _db;
        private LoginInfor LoginUser;
        private List<회사별메뉴> menulist;

        public ApproveController(ILogger<HomeController> logger, HanbizaContext db)
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
        // 30 OT결재
        [Authorize]
        public IActionResult Sub30()
        {
            Boolean checkLogin = CheckLogin();
            if (!checkLogin) return RedirectToAction("Login", "Account");

            ViewBag.menulist = menulist;
            //GetLoginUser();
            //_logger.LogInformation("sub30(): " + LoginUser.BizNum + " / " + LoginUser.StaffId + " / " + LoginUser.Dname);

            List<ApproveList> Alist = null;
            _db.LoadStoredProc("dbo.OT_approvalList").AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId)
               .AddParam("Dname", LoginUser.Dname).Exec(r => Alist = r.ToList<ApproveList>());
            //Console.WriteLine("sub4 list count: " + Alist.Count());
            if (Alist != null)
            {
                return View(Alist);
            }

            return View();
        }
        // 31 휴가결재
        [Authorize]
        public IActionResult Sub31()
        {
            Boolean checkLogin = CheckLogin();
            if (!checkLogin) return RedirectToAction("Login", "Account");

            ViewBag.menulist = menulist;
            //GetLoginUser();
            //_logger.LogInformation("sub30(): " + LoginUser.BizNum + " / " + LoginUser.StaffId + " / " + LoginUser.Dname);

            List<ApproveList> Alist = null; ;
            _db.LoadStoredProc("dbo.approvalList").AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId)
               .AddParam("Dname", LoginUser.Dname).Exec(r => Alist = r.ToList<ApproveList>());
            //Console.WriteLine("sub4 list count: " + Alist.Count());
            if (Alist != null)
            {
                return View(Alist);
            }

            return View();
        }
        // (공용)31-1 휴가 승인 프로세스
        [Route("/Approve/Sub31_1/{type}/{checkedVacId}")]
        public string Sub31_1(string type, string checkedVacId)
        {
            Boolean checkLogin = CheckLogin();
            string[] arrVacId = checkedVacId.Split(",");
            //_logger.LogInformation("sub4_1(): " + checkedVacId +"/" + arrVacId.Length);
            int count = 0;
            for (int i = 0; i < arrVacId.Length; i++)
            {
                var rs = _db.LoadStoredProc("apply_process_allow").AddParam("Type", type)
                            .AddParam("approveID", LoginUser.StaffId).AddParam("VacID", arrVacId[i]).ExecNonQuery();
                if (rs > 0) count++;
            }
            string result;
            if (count == arrVacId.Length)
            {
                result = "success";
                return result;
            }
            else
            {
                result = "fail";
                return result;
            }
        }
        // (공용)31-2 휴가 반려 프로세스
        [Route("/Approve/Sub31_2/{type}/{VacID}/{RereaSon}")]
        public string Sub31_2(string type, string VacID, string RereaSon)
        {
            Boolean checkLogin = CheckLogin();
            string result;
            _logger.LogInformation("sub31_2(): " + VacID + " / " + RereaSon);
            var rs = _db.LoadStoredProc("apply_process_reject").AddParam("Type", type)
                        .AddParam("approveID", LoginUser.StaffId).AddParam("VacID", VacID).AddParam("RereaSon", RereaSon).ExecNonQuery();
            if (rs > 0)
            {
                result = "success";
                return result;
            }
            result = "fail";
            return result;
        }
        // (공용)31-3 결재결과 가져오기
        [Route("/Approve/Sub31_3/{Type}/{Snal}/{Enal}")]
        public string Sub31_3(string type, string Snal, string Enal)
        {
            Boolean checkLogin = CheckLogin();
            var jsonString = "";

            List<Approve_History> AHlist = null; ;
            _db.LoadStoredProc("dbo.apply_resultHistory").AddParam("Type", type).AddParam("Snal", Snal).AddParam("Enal", Enal)
               .AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId).AddParam("Dname", LoginUser.Dname)
               .Exec(r => AHlist = r.ToList<Approve_History>());
            if (AHlist != null)
            {
                jsonString = JsonConvert.SerializeObject(AHlist);
                
                return jsonString;
            }
            jsonString = "fail";
            return jsonString;
        }
       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
