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

namespace HanbizaMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private LoginUser LoginUser;
        

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public void GetLoginUser()
        {
            if (HttpContext.Session.GetString("StaffId") != null )
            {
                var StaffId = (int)HttpContext.Session.GetInt32("StaffId");
                var BizNum = HttpContext.Session.GetString("BizNum");
                var LoginDate = HttpContext.Session.GetString("DateNow");
                LoginUser = new LoginUser
                {
                    StaffId = StaffId,
                    BizNum = BizNum,
                    LoginDate = LoginDate
                };
            }


        }


// 0. 로그인 후 첫 화면 : 공지사항 
        public IActionResult Index()
        {
            GetLoginUser();
            ViewBag.LoginUser = LoginUser;
            return View();
        }
// 1. 근태보기
        public IActionResult Sub1()
        {
            GetLoginUser();
            _logger.LogInformation("sub1(): "+ LoginUser.BizNum + " / " + LoginUser.StaffId + " / " + LoginUser.LoginDate);
            
            using (var db = new HanbizaContext()) {
                
                // 최근 근태기록 월 구하기
                db.LoadStoredProc("dbo.lastMonth").AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId)
                    .ExecScalar(out string dateMonth);

                ViewBag.최근월 = dateMonth;

                // 월별근태내역 - 근무/휴가
                var CulTable = from data in db.출퇴근기록집계표
                               where data.StaffId == LoginUser.StaffId
                               select data;
                
                foreach (var i in CulTable)
                {
                    ViewBag.기준일 = i.기준일;
                    ViewBag.근무일 = i.근무일;
                    ViewBag.결근일 = i.결근일;
                    ViewBag.휴무일 = i.휴무일;
                    ViewBag.주휴일 = i.주휴일;
                    ViewBag.유급휴일 = i.유급휴가휴일 + i.유급휴일;
                    ViewBag.무급휴가휴일 = i.무급휴가휴일;
                    ViewBag.유급주휴일 = i.유급주휴일;
                }

                // 월별근태내역 - 근무외시수
                List<TotalAttendence> totalTable = null;
                db.LoadStoredProc("dbo.totalAttendence").AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId).AddParam("lastMonth", dateMonth)
                  .Exec(r => totalTable = r.ToList<TotalAttendence>());

                foreach (var i in totalTable)
                {
                    ViewBag.총근로 = i.총근로;
                    ViewBag.소정근로 = i.소정근로;
                    ViewBag.근태조정 = i.근태조정;
                    ViewBag.초과근로 = i.초과근로;
                    ViewBag.연장근로 = i.연장근로;
                    ViewBag.야간근로 = i.야간근로;
                    ViewBag.휴일근로 = i.휴일근로;
                    ViewBag.휴일연장 = i.휴일연장;
                    ViewBag.휴일야간 = i.휴일야간;
                }

                // 출퇴근기록
                List<출퇴근기록> recordTable = null;
                db.LoadStoredProc("dbo.attendRecord").AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId).AddParam("lastMonth", dateMonth)
                    .Exec(r => recordTable = r.ToList<출퇴근기록>());

                ViewBag.recordTable = recordTable;

                if (CulTable != null && recordTable != null && totalTable != null)
                {
                    return View(recordTable);
                }

            }
            return View();
        }
        
// 2. OT신청
        public IActionResult Sub2(AddTimeList addtime)
        {
            GetLoginUser();

            _logger.LogInformation("sub2(): " + LoginUser.BizNum + " / " + LoginUser.StaffId);
    
                    
            
            using (var db = new HanbizaContext())
            {
                // OT 신청내역
                List<AddTimeList> OTlist = null;
                db.LoadStoredProc("dbo.OT_list").AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId)
                    .Exec(r => OTlist = r.ToList<AddTimeList>());

                if(OTlist.Count > 0)
                {
                    return View(OTlist);
                }
            }
                return View();
        }
//2-1 OT 신청 클릭
        public IActionResult Sub2_1(AddTimeList addtime)
        {
            
            _logger.LogInformation("sub2(addtime): " + addtime.Gubun + " / " + addtime.Snal + " / " + addtime.Enal); // 신청x
            
            return new RedirectResult("/Home/Sub2");
        }
// 3. 휴가신청
        public IActionResult Sub3()
        {
            GetLoginUser();
            _logger.LogInformation("sub3(): " + LoginUser.BizNum + " / " + LoginUser.StaffId);

            using (var db = new HanbizaContext())
            {
                List<Vacation_List> Vlist = null;
                db.LoadStoredProc("dbo.vacation_getVacation").AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId)
                    .Exec(r => Vlist = r.ToList<Vacation_List>());
                
                if (Vlist != null)
                {
                    return View(Vlist);
                }
            }

                return View();
        }
        // 3_1. ajax 메소드
        [Route("/Home/Sub3_1/{SearchKey}/{SearchWord}")]
        public IActionResult Sub3_1(string SearchKey, string SearchWord)
        {
            GetLoginUser();
            _logger.LogInformation("sub3_1(): " + LoginUser.BizNum + " / " + LoginUser.StaffId + " / " + SearchKey + " / " + SearchWord);
            var jsonString = "";

            if (SearchKey != null && SearchWord != null)
            {
                if (!SearchKey.Equals("") && !SearchWord.Equals(""))
                {
                    using (var db = new HanbizaContext())
                    {
                        List<Approver> Datatable = null;
                        db.LoadStoredProc("vacation_getApprover").AddParam("BizNum", LoginUser.BizNum).AddParam("SearchKey", SearchKey).AddParam("SearchWord", SearchWord)
                          .Exec(r => Datatable = r.ToList<Approver>());
                        
                        jsonString = JsonConvert.SerializeObject(Datatable);
                        _logger.LogInformation("json1: "+jsonString);
                    }
                }
            }
            return new JsonResult(jsonString);
        }
        // 3_2. ajax 메소드
        [Route("/Home/Sub3_2/{VacID}")]
        public IActionResult Sub3_2(string VacID)
        {
            GetLoginUser();
            _logger.LogInformation("sub3_2(): " + LoginUser.BizNum + " / " + LoginUser.StaffId + " / " + VacID);
            var jsonString = "";

            using (var db = new HanbizaContext())
            {
                List<Vacation_ProcessDetail> VPList = null; ;
                db.LoadStoredProc("dbo.vacation_getDetail").AddParam("VacID", VacID)
                    .Exec(r => VPList = r.ToList<Vacation_ProcessDetail>());

                jsonString = JsonConvert.SerializeObject(VPList);
                _logger.LogInformation("json2: " + jsonString);
            }
                return  new JsonResult(jsonString);
        }

            // 4. 휴가결재
        public IActionResult Sub4()
        {
            GetLoginUser();
            _logger.LogInformation("sub4(): " + LoginUser.BizNum + " / " + LoginUser.StaffId);
            using (var db = new HanbizaContext())
            {
                List<ApproveList> Alist = null; ;
                db.LoadStoredProc("dbo.approvalList").AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId)
                    .Exec(r => Alist = r.ToList<ApproveList>());
                Console.WriteLine("sub4 list count: "+Alist.Count());
                if (Alist != null)
                {
                    return View(Alist);
                }
            }

            return View();
        }

// 5. 연차보기
        public IActionResult Sub5()
        {
            GetLoginUser();
            _logger.LogInformation("sub5(): " + LoginUser.BizNum + " / " + LoginUser.StaffId);
            using (var db = new HanbizaContext())
            {
                List<연차대장> vacationRecord = null;
                db.LoadStoredProc("dbo.countVacation").AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId)
                    .Exec(r => vacationRecord = r.ToList<연차대장>());

                foreach (var i in vacationRecord)
                {
                    ViewBag.입사일 = i.입사일.ToShortDateString();
                    ViewBag.연차발생일 = i.연차발생일.ToShortDateString();
                    ViewBag.근속연수 = i.근속년수;
                    ViewBag.발생연차 = i.발생연차;
                    ViewBag.사용연차 = i.발생연차 - i.잔여일수;
                    ViewBag.잔여연차 = i.잔여일수;
                    ViewBag.Regdate = i.Regdate.ToShortDateString();
                }

                List<휴가대장> vacationList = null;
                db.LoadStoredProc("dbo.usingVacation").AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId)
                    .Exec(r => vacationList = r.ToList<휴가대장>());

                ViewBag.vacationList = vacationList;

                if (vacationRecord != null && vacationList != null)
                {
                    return View(vacationList);
                }
            }
                return View();
        }

// 6. 급여명세서
        public IActionResult Sub6()
        {
            GetLoginUser();
            _logger.LogInformation("sub6(): " + LoginUser.BizNum + " / " + LoginUser.StaffId);

            List<PayList> plist = null;
            using (var db = new HanbizaContext())
            {
                db.LoadStoredProc("dbo.payment_lastMonth").AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId)
                    .Exec(r => plist = r.ToList<PayList>());
               
                    Console.WriteLine(plist[0].Yyyymm +"년 "+plist[0].Ncount+"회차");

                var yyyymm = plist[0].Yyyymm;
                var ncount = plist[0].Ncount;

                db.LoadStoredProc("dbo.payment_getPayment").AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId)
                    .AddParam("Yyyymm", yyyymm).AddParam("Ncount", ncount).Exec(r => plist = r.ToList<PayList>());

                int a = 0;
                int b = 0;
                int i = 0;
                foreach (var item in plist)
                {
                    item.SSvalue = string.Format("{0:n0}", item.Svalue);
                    Console.WriteLine(item.Slist +"/"+item.SSvalue+"/"+item.Gubun+"/"+item.Fsort +"/  "+i);
                    i++;    
                    if (item.Gubun.Equals("0") && item.Fsort == 0){ a++; }
                    if (item.Gubun.Equals("1") && item.Fsort == 0){ b++; }
                }
                Console.WriteLine(a +" / "+b);
                if (a >= b)
                {
                    ViewBag.Crows = a;
                }
                else if (b > a)
                {
                    ViewBag.Crows = b;
                }

            }

            return View(plist);
        }
        
      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
