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
using System.Dynamic;

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
            _db.LoadStoredProc("dbo.approvalList").AddParam("Type", "OT")
               .AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId).AddParam("Dname", LoginUser.Dname)
               .Exec(r => Alist = r.ToList<ApproveList>());
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
            _db.LoadStoredProc("dbo.approvalList").AddParam("Type", "vacation")
               .AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId).AddParam("Dname", LoginUser.Dname)
               .Exec(r => Alist = r.ToList<ApproveList>());
            //Console.WriteLine("sub4 list count: " + Alist.Count());
            if (Alist != null)
            {
                return View(Alist);
            }

            return View();
        }
        // 33 기타결재
        [Authorize]
        public IActionResult Sub33()
        {
            Boolean checkLogin = CheckLogin();
            if (!checkLogin) return RedirectToAction("Login", "Account");

            ViewBag.menulist = menulist;
            //GetLoginUser();
            //_logger.LogInformation("sub30(): " + LoginUser.BizNum + " / " + LoginUser.StaffId + " / " + LoginUser.Dname);

            List<ApproveList> Alist = null; ;
            _db.LoadStoredProc("dbo.approvalList").AddParam("Type", "ETC")
               .AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId).AddParam("Dname", LoginUser.Dname)
               .Exec(r => Alist = r.ToList<ApproveList>());
            //Console.WriteLine("sub4 list count: " + Alist.Count());
            if (Alist != null)
            {
                return View(Alist);
            }

            return View();
        }


        // (공용)31-1 결재 승인 프로세스
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
        // (공용)31-2 결재 반려 프로세스
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
        // (공용)31-2-1 결재 반려 취소 프로세스
        [Route("/Approve/Sub31_2_1/{type}/{VacID}")]
        public string Sub31_2_1(string type, string VacID)
        {
            Boolean checkLogin = CheckLogin();
            string result;
            _logger.LogInformation("sub31_2_1(): " + VacID);
            var rs = _db.LoadStoredProc("apply_process_reject_Cancel").AddParam("Type", type)
                        .AddParam("StaffID", LoginUser.StaffId).AddParam("VacID", VacID).ExecNonQuery();
            if (rs > 0)
            {
                result = "success";
                return result;
            }
            result = "fail";
            return result;
        }


        // (공용)31-3 결재 결과 가져오기
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



        // 32 인사평가
        [Authorize]
        [Route("/Approve/Sub32")]
        [Route("/Approve/Sub32/{HRsubmit}")]
        //[Route("/Approve/Sub32/{dateYear}")]
        [Route("/Approve/Sub32/{HRsubmit}/{dateYear}")]
        public IActionResult Sub32(string HRsubmit, string dateYear)
        {
            Console.WriteLine("dateY:" + dateYear);
            Boolean checkLogin = CheckLogin();
            if (!checkLogin) return RedirectToAction("Login", "Account");
            //dynamic mymodel = new ExpandoObject();
            ViewBag.menulist = menulist;
            //_logger.LogInformation("sub32(): " + LoginUser.BizNum + " / " + LoginUser.StaffId + " / " + LoginUser.Dname);

            if (HRsubmit == null) HRsubmit = "";

            List<인사평가> Years = null;
            if (dateYear == null)
            {
                _db.LoadStoredProc("dbo.lastYear")
                   .AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId).AddParam("Dname", LoginUser.Dname)
                   .Exec(r => Years = r.ToList<인사평가>());
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
                _db.LoadStoredProc("dbo.lastYear")
                   .AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId).AddParam("Dname", LoginUser.Dname)
                   .Exec(r => Years = r.ToList<인사평가>());
                ViewBag.선택년 = dateYear;
                ViewBag.Years = Years;
            }
            //Console.WriteLine("dateY:"+dateYear);
            List<인사평가> HRlist = null;
            _db.LoadStoredProc("dbo.HR_Assessee_list")
               .AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId).AddParam("Dname", LoginUser.Dname).AddParam("dateYear", dateYear)
               .Exec(r => HRlist = r.ToList<인사평가>());
            
            ViewBag.HRsubmit = HRsubmit;
            //Console.WriteLine(Years.Count);
            if (HRlist != null) return View(HRlist);

            return View();
        }
        // 32_1 인사평가 평가시작 세부내용
        [Authorize]
        //[Route("/Approve/Sub32_1/{flag}/{BigSubject}/{Gubun}/{AssesseeID}")]
        [Route("/Approve/Sub32_1/{flag}/{BigSubject}/{AssesseeID}")]
        public IActionResult Sub32_1(string flag, string BigSubject, /*string Gubun,*/ int AssesseeID)
        {
            Boolean checkLogin = CheckLogin();
            if (!checkLogin) return RedirectToAction("Login", "Account");
            ViewBag.menulist = menulist;
            if (BigSubject.Contains("-")) BigSubject = BigSubject.Replace("-", "/");
            
            List<인사평가> HR_Detail_list = null;
            _db.LoadStoredProc("dbo.HR_Detail_list").AddParam("BigSubject", BigSubject)/*.AddParam("Gubun", Gubun)*/.AddParam("AssesseeID", AssesseeID)
               .AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId).AddParam("Dname", LoginUser.Dname)
               .Exec(r => HR_Detail_list = r.ToList<인사평가>());

            ViewBag.flag = flag;
            ViewBag.AssesseeID = AssesseeID;
            ViewBag.BigSubject = BigSubject;
            
            if (HR_Detail_list != null) return View(HR_Detail_list);

            return View();
            /*
            var a = 1;
            var b = 1;
            var c = 1;

            for (c = a; c < HR_Detail_list.Count;)
            {
                Console.WriteLine("다른 테이블");

                for (b = a; b < HR_Detail_list.Count;)
                {
                    if (b == -1)
                    {
                        Console.WriteLine(a);
                        c = a + 1;
                        b = a + 1;
                        a = a + 1;
                        break;
                    }
                    if (a == HR_Detail_list.Count)
                    {
                        goto Out;
                    }
                    Console.WriteLine("1: " + HR_Detail_list[b].평가기준 + "a: " + a + "/ b : " + b);
                    for (a = b; a < HR_Detail_list.Count;)
                    {
                        Console.Write("2:" + HR_Detail_list[a - 1].등급목록);
                        if (a == HR_Detail_list.Count)
                        {
                            goto Out;
                        }
                        else if (HR_Detail_list[a - 1].구분 == HR_Detail_list[a].구분)   // 같은 구분
                        {
                            if (HR_Detail_list[a - 1].답변ID == HR_Detail_list[a].답변ID)    // 같은 질문
                            {
                                a++;
                            }
                            else // 다른 질문으로  넘어갈 때
                            {
                                b = a + 1;
                                break;
                            }
                        }
                        else  // 다른 구분
                        {
                            Console.WriteLine("a= " + a + " / b= " + b + " : 다른구분");
                            b = -1;
                            break;
                        }
                    }
                }
            }
        Out: Console.WriteLine("끝");
            */ // console 테스트
        }
        // 32_2 인사평가 임시 저장 - 마감: T
        [Authorize]
        [Route("/Approve/Sub32_2/{QseqIDs}/{AseqIDs}")]
        public string Sub32_2(string QseqIDs, string AseqIDs)
        {
            Boolean checkLogin = CheckLogin();
            ViewBag.menulist = menulist;
            //Console.WriteLine("T저장: "+QseqIDs + AseqIDs);
            var a = _db.LoadStoredProc("dbo.HR_Save")
                       .AddParam("QseqIDs", QseqIDs).AddParam("AseqIDs", AseqIDs).AddParam("StaffName", LoginUser.StaffName)
                       .ExecNonQuery();
            var rs = "fail";
            if (a > 0) rs = "success";

            return rs;
        }
        // 32_3 인사평가 제출 - 마감: Y
        [Authorize]
        [Route("/Approve/Sub32_3/{AssesseeID}/{BigSubject}")]
        public string Sub32_3(string AssesseeID, string BigSubject)
        {
            Boolean checkLogin = CheckLogin();
            ViewBag.menulist = menulist;
            //Console.WriteLine("Y: "+BigSubject);

            if (BigSubject.Contains("-")) BigSubject = BigSubject.Replace("-", "/");
            var a = _db.LoadStoredProc("dbo.HR_Submit")
                       .AddParam("AssesseeID", AssesseeID).AddParam("BigSubject", BigSubject).AddParam("StaffName", LoginUser.StaffName)
                       .AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId).AddParam("Dname", LoginUser.Dname)
                       .ExecNonQuery();
            var rs = "fail";
            if (a > 0) rs = "success";

            return rs;
        }

        // 32_4 인사평가 제출 전 검사
        [Authorize]
        [Route("/Approve/Sub32_4/{AssesseeID}/{BigSubject}")]
        public string Sub32_4(string AssesseeID, string BigSubject)
        {
            Boolean checkLogin = CheckLogin();
            ViewBag.menulist = menulist;

            List<인사평가> status = null;
            var checkString = "";
            if (BigSubject.Contains("-")) BigSubject = BigSubject.Replace("-", "/");
            _db.LoadStoredProc("dbo.HR_CheckAnswer")
                       .AddParam("AssesseeID", AssesseeID).AddParam("BigSubject", BigSubject)
                       .AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId).AddParam("Dname", LoginUser.Dname)
                       .Exec(r => status = r.ToList<인사평가>());

            foreach (var item in status)
            {
                checkString = item.마감;
            }

            var rs = "fail";
            if (checkString.Equals("T") ) rs = "success";

            return rs;
        }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
