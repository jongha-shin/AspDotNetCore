using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HanbizaMVC.Models;
using System;
using StoredProcedureEFCore;
using System.Collections.Generic;
using HanbizaMVC.ViewModel;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HanbizaMVC.Controllers
{
    public class ApplyController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HanbizaContext _db;
        private LoginInfor LoginUser;
        private List<회사별메뉴> menulist;

        public ApplyController(ILogger<HomeController> logger, HanbizaContext db)
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
        // 20 OT 신청
        [Authorize]
        [Route("/Apply/Sub20")]
        [Route("/Apply/Sub20/{secondTab}")]
        [Route("/Apply/Sub20/{secondTab}/{dateYear}")]
        public IActionResult Sub20(string secondTab,string dateYear)
        {
            Boolean checkLogin = CheckLogin();
            if (!checkLogin) return RedirectToAction("Login", "Account");

            ViewBag.menulist = menulist;
            //_logger.LogInformation("sub2(): " + LoginUser.BizNum + " / " + LoginUser.StaffId);

            if (secondTab == null) secondTab = "";

            List<AddTimeList> Years = null;
            if (dateYear == null)
            {
                _db.LoadStoredProc("dbo.last_Year").AddParam("Type", "OT")
                   .AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId).AddParam("Dname", LoginUser.Dname)
                   .Exec(r => Years = r.ToList<AddTimeList>());
                if (Years.Count() == 0)
                {
                    dateYear = DateTime.Now.ToString("yyyy");
                }
                else
                {
                    dateYear = Years[0].년;
                }
                ViewBag.선택년 = dateYear;
                ViewBag.Years = Years;
            }
            else
            {
                _db.LoadStoredProc("dbo.last_Year").AddParam("Type", "OT")
                   .AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId).AddParam("Dname", LoginUser.Dname)
                   .Exec(r => Years = r.ToList<AddTimeList>());
                ViewBag.선택년 = dateYear;
                ViewBag.Years = Years;
            }

            //var yearParam = new DateTime(int.Parse(dateYear), 01, 01);
            // OT 신청내역
            List<AddTimeList> OTlist = null;
            _db.LoadStoredProc("dbo.apply_getApplication_Year").AddParam("Type", "OT").AddParam("Year", dateYear)
                .AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId).AddParam("Dname", LoginUser.Dname)
                .Exec(r => OTlist = r.ToList<AddTimeList>());

            ViewBag.secondTab = secondTab;
            if (OTlist.Count > 0)
            {
                return View(OTlist);
            }

            return View();
        }
        // 20-1 OT 신청 클릭
        [Authorize]
        public string Sub20_1(AddTimeList addtime)
        {
            Boolean checkLogin = CheckLogin();
            string rsString;
            //_logger.LogInformation("sub2(addtime): " + addtime.Gubun + " / " + addtime.Snal + " / " + addtime.Enal + " / " + addtime.Reason); // 신청x

            var rs = _db.LoadStoredProc("dbo.apply_insert_OT")
                        .AddParam("Dname", LoginUser.Dname).AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId).AddParam("StaffName", LoginUser.StaffName)
                        .AddParam("Gubun", addtime.Gubun).AddParam("Snal", addtime.Snal).AddParam("Enal", addtime.Enal).AddParam("Reason", addtime.Reason).AddParam("ProCDeep", addtime.ProCDeep)
                        .ExecNonQuery();
            if (rs > 0)
            {
                rsString = "success";
                return rsString;
            }
            rsString = "fail";
            return rsString;
        }
        
        // 21. 휴가신청
        [Authorize]
        [Route("/Apply/Sub21")]
        [Route("/Apply/Sub21/{secondTab}")]
        [Route("/Apply/Sub21/{secondTab}/{dateYear}")]
        public IActionResult Sub21(string secondTab, string dateYear)
        {
            Boolean checkLogin = CheckLogin();
            if (!checkLogin) return RedirectToAction("Login", "Account");

            ViewBag.menulist = menulist;
            //GetLoginUser();
            //_logger.LogInformation("sub21(): " + LoginUser.BizNum + " / " + LoginUser.StaffId);

            if (secondTab == null) secondTab = "";

            List<Vacation_List> Years = null;
            if (dateYear == null)
            {
                _db.LoadStoredProc("dbo.last_Year").AddParam("Type", "vacation")
                   .AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId).AddParam("Dname", LoginUser.Dname)
                   .Exec(r => Years = r.ToList<Vacation_List>());
                if (Years.Count() == 0)
                {
                    dateYear = DateTime.Now.ToString("yyyy");
                }
                else
                {
                    dateYear = Years[0].년;
                }
                ViewBag.선택년 = dateYear;
                ViewBag.Years = Years;
            }
            else
            {
                _db.LoadStoredProc("dbo.last_Year").AddParam("Type", "vacation")
                   .AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId).AddParam("Dname", LoginUser.Dname)
                   .Exec(r => Years = r.ToList<Vacation_List>());
                ViewBag.선택년 = dateYear;
                ViewBag.Years = Years;
            }

            List<Vacation_List> Vlist = null;
            _db.LoadStoredProc("dbo.apply_getApplication_Year").AddParam("Type", "vacation").AddParam("Year", dateYear)
                .AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId).AddParam("Dname", LoginUser.Dname)
                .Exec(r => Vlist = r.ToList<Vacation_List>());

            ViewBag.secondTab = secondTab;
            if (Vlist != null)
            {
                return View(Vlist);
            }

            return View();
        }
        // (공용) 21_1. 휴가 결재자 찾기
        [Authorize]
        [Route("/Apply/Sub21_1/{SearchWord}/{Step_num}/{StaffList}")]
        public IActionResult Sub21_1(string SearchWord, string Step_num, string StaffList)
        {
            Boolean checkLogin = CheckLogin();
            if (!checkLogin) return RedirectToAction("Login", "Account");
            ViewBag.menulist = menulist;
            //_logger.LogInformation("sub21_1(): " + SearchWord + " / " + Step_num);
            //Console.WriteLine("list: " + StaffList);
            var jsonString = "";

            if (SearchWord != null)
            {
                if (!SearchWord.Equals(""))
                {
                    List<Approver> Datatable = null;
                    _db.LoadStoredProc("dbo.apply_getApprover")
                       .AddParam("BizNum", LoginUser.BizNum).AddParam("StaffID", LoginUser.StaffId).AddParam("Dname", LoginUser.Dname)
                       .AddParam("SearchWord", SearchWord)/*.AddParam("Step_num", Step_num)*/.AddParam("StaffList", StaffList)
                       .Exec(r => Datatable = r.ToList<Approver>());

                    jsonString = JsonConvert.SerializeObject(Datatable);
                    //_logger.LogInformation("json1: " + jsonString);
                }
            }
            return new JsonResult(jsonString);
        }
        // (공용)21_2. 결재 과정 상세 보기
        [Authorize]
        [Route("/Apply/Sub21_2/{type}/{SeqID}")]
        public IActionResult Sub21_2(string type, string SeqID)
        {
            Boolean checkLogin = CheckLogin();
            if (!checkLogin) return RedirectToAction("Login", "Account");

            ViewBag.menulist = menulist;
            // GetLoginUser();
            //_logger.LogInformation("sub3_2(): " + LoginUser.BizNum + " / " + LoginUser.StaffId + " / " + VacID);
            var jsonString = "";

            List<apply_ProcessDetail> VPList = null; ;
            _db.LoadStoredProc("dbo.apply_getDetail").AddParam("Type", type).AddParam("applyID", SeqID)
                .Exec(r => VPList = r.ToList<apply_ProcessDetail>());

            jsonString = JsonConvert.SerializeObject(VPList);
            //_logger.LogInformation("json2: " + jsonString);

            return new JsonResult(jsonString);
        }
        // 21_3 휴가신청
        public string Sub21_3(Vacation_List VaInfo)
        {
            Boolean checkLogin = CheckLogin();
            string rsString;
            //_logger.LogInformation("sub21_3(): " + LoginUser.BizNum + " / " + LoginUser.StaffId);

            var rs = _db.LoadStoredProc("dbo.apply_insert_Vacation")
                        .AddParam("Dname", LoginUser.Dname).AddParam("BizNum", LoginUser.BizNum)
                        .AddParam("StaffId", LoginUser.StaffId).AddParam("Vicname", VaInfo.Vicname).AddParam("UseTime", VaInfo.UseTime)
                        .AddParam("Snal", VaInfo.Snal).AddParam("Enal", VaInfo.Enal).AddParam("ProCDeep", VaInfo.ProCDeep)
                        .AddParam("VicReaSon", VaInfo.VicReason)
                        .ExecNonQuery();
            //Console.WriteLine("Sub2_1 rs: " + rs);

            if (rs > 0)
            {
                rsString = "success";
                return rsString;
            }

            rsString = "fail";
            return rsString;
        }
        // (공용) 21_4 등록된 휴가정보 각 결재자에게 전송
        public string Sub21_4(Approve_Params approve_Params)
        {
            Boolean checkLogin = CheckLogin();
            //_logger.LogInformation("sub21_4(): " + LoginUser.BizNum + " / " + LoginUser.StaffId + " / " + approve_Params.StaffID1);
            string rsString;

            var rs = _db.LoadStoredProc("dbo.apply_insertEachApprover").AddParam("Type", approve_Params.Type)
                     .AddParam("ProCDeep", approve_Params.ProCDeep).AddParam("BizNum", LoginUser.BizNum).AddParam("Dname", LoginUser.Dname).AddParam("StaffName", LoginUser.StaffName)
                     .AddParam("StaffID1", approve_Params.StaffID1).AddParam("StaffID2", approve_Params.StaffID2).AddParam("StaffID3", approve_Params.StaffID3).AddParam("StaffID4", approve_Params.StaffID4).AddParam("StaffID5", approve_Params.StaffID5)
                     .AddParam("Sign1", approve_Params.Sign1).AddParam("Sign2", approve_Params.Sign2).AddParam("Sign3", approve_Params.Sign3).AddParam("Sign4", approve_Params.Sign4).AddParam("Sign5", approve_Params.Sign5)
                     .ExecNonQuery();
            if (rs > 0)
            {
                rsString = "success";
                return rsString;
            }

            rsString = "fail";
            return rsString;
        }
        // 21-5 (공용)휴가결재 진행 전 취소, 등록된 휴가seq 삭제
        [Route("/Apply/Sub21_5/{type}/{seqid}")]
        public string Sub21_5(string type, string seqid)
        {
            Boolean checkLogin = CheckLogin();
            //_logger.LogInformation("sub21_5(): " + LoginUser.BizNum + " / " + LoginUser.StaffId + " / " + seqid);
            string rsString = "fail";

            var rs = _db.LoadStoredProc("dbo.apply_cancel").AddParam("SEQID", seqid).AddParam("Type", type).ExecNonQuery();

            if (rs > 0)
            {
                rsString = "success";
                return rsString;
            }
            return rsString;
        }
        // (공용)21-6 휴가 히스토리에서 결재자 정보 가져오기
        [Route("/Apply/Sub21_6/{type}")]
        public IActionResult Sub21_6(string type)
        {
            Boolean checkLogin = CheckLogin();
            if (!checkLogin) return RedirectToAction("Login", "Account");
            ViewBag.menulist = menulist;

            var jsonString = "";
            List<Approver> Alist = null;
            _db.LoadStoredProc("dbo.apply_getPreApprover").AddParam("Type", type)
                .AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId).AddParam("Dname", LoginUser.Dname)
                .Exec(r => Alist = r.ToList<Approver>());
            jsonString = JsonConvert.SerializeObject(Alist);
            return new JsonResult(jsonString);
        }

        // 기타신청
        [Authorize]
        [Route("/Apply/Sub22")]
        [Route("/Apply/Sub22/{secondTab}/{dateYear}")]
        public IActionResult Sub22(string secondTab, string dateYear)
        {
            Boolean checkLogin = CheckLogin();
            if (!checkLogin) return RedirectToAction("Login", "Account");

            if (secondTab == null) secondTab = "";

            List<Etc_List> Years = null;
            if (dateYear == null)
            {
                _db.LoadStoredProc("dbo.last_Year").AddParam("Type", "ETC")
                   .AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId).AddParam("Dname", LoginUser.Dname)
                   .Exec(r => Years = r.ToList<Etc_List>());
                if (Years.Count() == 0)
                {
                    dateYear = DateTime.Now.ToString("yyyy");
                }
                else
                {
                    dateYear = Years[0].년;
                }
                ViewBag.선택년 = dateYear;
                ViewBag.Years = Years;
            }
            else
            {
                _db.LoadStoredProc("dbo.last_Year").AddParam("Type", "ETC")
                   .AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId).AddParam("Dname", LoginUser.Dname)
                   .Exec(r => Years = r.ToList<Etc_List>());
                ViewBag.선택년 = dateYear;
                ViewBag.Years = Years;
            }

            // ETC 신청내역
            List<Etc_List> ETClist = null;
            _db.LoadStoredProc("dbo.apply_getApplication_Year").AddParam("Type", "ETC").AddParam("Year", dateYear)
                .AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId).AddParam("Dname", LoginUser.Dname)
                .Exec(r => ETClist = r.ToList<Etc_List>());

            ViewBag.menulist = menulist;
            ViewBag.secondTab = secondTab;
            if (ETClist.Count > 0) return View(ETClist);

            return View();
        }

        // 22-1 기타 신청
        [HttpPost]
        [Authorize]
        public string Sub22_1(Etc_List etc)
        {
            Boolean checkLogin = CheckLogin();
            string rsString;
            Console.WriteLine("사유길이 : "+etc.EtcReason.Length);
            var rs = _db.LoadStoredProc("dbo.apply_insert_ETC")
                        .AddParam("Dname", LoginUser.Dname).AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId).AddParam("StaffName", LoginUser.StaffName)
                        .AddParam("Gubun1", etc.Gubun1).AddParam("Gubun2", etc.Gubun2).AddParam("Snal", etc.Snal).AddParam("Enal", etc.Enal).AddParam("EtcReason", etc.EtcReason).AddParam("ProCDeep", etc.ProCDeep)
                        .ExecNonQuery();
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
