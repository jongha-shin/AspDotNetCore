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
using System.Dynamic;
using System.Security.Claims;

namespace HanbizaMVC.Controllers
{
    public class RecordController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HanbizaContext _db;
        private LoginInfor LoginUser;
        private List<회사별메뉴> menulist;

        public RecordController(ILogger<HomeController> logger, HanbizaContext db)
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

        // 1. 근태보기
        [Authorize]
        [Route("/Record/Sub10")]
        [Route("/Record/Sub10/{dateMonth}")]
        public IActionResult Sub10(string dateMonth)
        {
            Boolean checkLogin = CheckLogin();
            if (!checkLogin) return RedirectToAction("LogIn", "Account");

            ViewBag.menulist = menulist;
            //_logger.LogInformation("sub1(): " + LoginUser.BizNum + " / " + LoginUser.StaffId);
            // 최근 근태기록 월 구하기
            List<출퇴근기록> Months = null;
            if (dateMonth == null)
            {
                dateMonth = "";
                _db.LoadStoredProc("dbo.lastMonth")
                   .AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId).AddParam("Dname", LoginUser.Dname)
                   .Exec(r => Months = r.ToList<출퇴근기록>());
                if (Months.Count() == 0)
                {
                    dateMonth = DateTime.Now.ToString("yyyy-MM");
                }
                else
                {
                    dateMonth = Months[0].월;
                }
                ViewBag.선택월 = dateMonth;
            }
            else
            {
                _db.LoadStoredProc("dbo.lastMonth")
                   .AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId).AddParam("Dname", LoginUser.Dname)
                   .Exec(r => Months = r.ToList<출퇴근기록>());
                ViewBag.선택월 = dateMonth;
            }
            //Console.WriteLine("1 선택월: " + dateMonth);

            // 월별근태내역 - 근무/휴가
            List<출퇴근기록집계표> CulTable = null;
            //Console.WriteLine(dateMonth);
            _db.LoadStoredProc("attend_MonthlyRecord").AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId)
                .AddParam("Dname", LoginUser.Dname).AddParam("lastMonth", dateMonth).Exec(r => CulTable = r.ToList<출퇴근기록집계표>());

            //var CulTable = from data in _db.출퇴근기록집계표
            //               where data.StaffId == LoginUser.StaffId 
            //               select data;
            if(CulTable.Count == 0)
            {
                    ViewBag.기준일 = 0;
                    ViewBag.근무일 = 0;
                    ViewBag.결근일 = 0;
                    ViewBag.휴무일 = 0;
                    ViewBag.주휴일 = 0;
                    ViewBag.유급휴일 = 0;
                    ViewBag.무급휴가휴일 = 0;
                    ViewBag.유급주휴일 = 0;
            }
            else
            {
                foreach (var i in CulTable)
                {
                    ViewBag.기준일 = i.기준일;
                    ViewBag.근무일 = i.근무일;
                    ViewBag.결근일 = i.결근일;
                    ViewBag.휴무일 = i.휴무일;
                    ViewBag.주휴일 = i.주휴일;
                    ViewBag.유급휴일 = i.유급휴가휴일; //+ i.유급휴일;
                    ViewBag.무급휴가휴일 = i.무급휴가휴일;
                    ViewBag.유급주휴일 = i.유급주휴일;
                }
            }

            // 월별근태내역 - 근무외시수
            List<TotalAttendence> totalTable = null;
            _db.LoadStoredProc("dbo.totalAttendence").AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId).AddParam("lastMonth", dateMonth)
               .AddParam("Dname", LoginUser.Dname).Exec(r => totalTable = r.ToList<TotalAttendence>());

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

            return View(Months);
        }

        [Authorize]
        [Route("/Record/Sub10_1")]
        [Route("/Record/Sub10_1/{dateMonth}")]
        public IActionResult Sub10_1(string dateMonth)
        {
            Boolean checkLogin = CheckLogin();
            if (!checkLogin) return RedirectToAction("Login", "Account");

            ViewBag.menulist = menulist;
            dynamic mymodel = new ExpandoObject();
            // 최근 근태기록 월 구하기
            List<출퇴근기록> Months = null;
            if (dateMonth == null)
            {
                dateMonth = "";
                _db.LoadStoredProc("dbo.lastMonth").AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId)
                    .AddParam("Dname", LoginUser.Dname).Exec(r => Months = r.ToList<출퇴근기록>());

                dateMonth = Months[0].월;
                ViewBag.선택월 = dateMonth;
            }
            else
            {
                _db.LoadStoredProc("dbo.lastMonth").AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId)
                   .AddParam("Dname", LoginUser.Dname).Exec(r => Months = r.ToList<출퇴근기록>());
                ViewBag.선택월 = dateMonth;
            }
            //Console.WriteLine("1_1 선택월: " + dateMonth);
            // 출퇴근기록
            List<출퇴근기록> recordTable = null;
            _db.LoadStoredProc("dbo.attendRecord").AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId).AddParam("lastMonth", dateMonth)
                .AddParam("Dname", LoginUser.Dname).Exec(r => recordTable = r.ToList<출퇴근기록>());

            mymodel.monthList = Months;
            mymodel.recordTable = recordTable;
            return View(mymodel);
        }

        // 5. 연차보기
        [Authorize]
        public IActionResult Sub11()
        {
            Boolean checkLogin = CheckLogin();
            if (!checkLogin) return RedirectToAction("Login", "Account");

            ViewBag.menulist = menulist;
            //GetLoginUser();
            //_logger.LogInformation("sub11(): " + LoginUser.BizNum + " / " + LoginUser.StaffId);

            List<연차대장> vacationRecord = null;
            _db.LoadStoredProc("dbo.countVacation").AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId)
               .AddParam("Dname", LoginUser.Dname).Exec(r => vacationRecord = r.ToList<연차대장>());

            foreach (var i in vacationRecord)
            {
                ViewBag.입사일 = string.Format("{0:yyyy-MM-dd}", i.입사일);
                ViewBag.연차발생일 = string.Format("{0:yyyy-MM-dd}", i.연차발생일);
                ViewBag.근속연수 = i.근속년수;
                ViewBag.이월조정추가 = i.이월조정추가;

                if (i.연차구분.Equals("N")) 
                {
                    // 입사일 기준
                    ViewBag.사용연차 = i.발생연차 + i.이월조정추가 - i.잔여일수;
                }
                else
                {
                    // 회계년 기준
                    ViewBag.사용연차 = i.발생연차 - i.잔여일수;
                    //ViewBag.발생연차 = i.발생연차 + i.조정추가;
                }
                ViewBag.발생연차 = i.발생연차;
                if (i.조정추가 == null) i.조정추가 = 0;
                ViewBag.조정추가 = i.조정추가;
                ViewBag.Regdate = string.Format("{0:yyyy-MM-dd}", i.Regdate);
                ViewBag.잔여연차 = i.잔여일수;
                ViewBag.연차구분 = i.연차구분;
            }

            List<휴가대장> vacationList = null;
            _db.LoadStoredProc("dbo.usingVacation").AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId)
                .AddParam("Dname", LoginUser.Dname).Exec(r => vacationList = r.ToList<휴가대장>());

            ViewBag.vacationList = vacationList;

            if (vacationRecord != null && vacationList != null)
            {
                return View(vacationList);
            }

            return View();
        }

        // 6. 급여명세서
        [Authorize]
        [Route("/Record/Sub12")]
        [Route("/Record/Sub12/{Yyyymm}/{Ncount}")]
        public IActionResult Sub12(string Yyyymm, string Ncount)
        {
            Boolean checkLogin = CheckLogin();
            if (!checkLogin) return RedirectToAction("Login", "Account");

            ViewBag.menulist = menulist;
            //_logger.LogInformation("sub12(): " + LoginUser.BizNum + " / " + LoginUser.StaffId);

            dynamic mymodel = new ExpandoObject();
            List<PayList> plist = null;
            List<PayList> monthList = null;
            if (Yyyymm == null || Ncount == null)
            {
                // 초기 세팅
                _db.LoadStoredProc("dbo.payment_lastMonth").AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId)
                   .AddParam("Dname", LoginUser.Dname).Exec(r => monthList = r.ToList<PayList>());

                // Console.WriteLine(plist[0].Yyyymm + "년 " + plist[0].Ncount + "회차");
                if(monthList.Count == 0)
                {
                    Yyyymm = DateTime.Now.ToString("yyyy-MM");
                    Ncount = "0";
                }
                else
                {
                    Yyyymm = monthList[0].Yyyymm;
                    Ncount = monthList[0].Ncount+"";
                }

                ViewBag.선택월 = Yyyymm;
                ViewBag.선택회차 = Ncount;
                //Console.WriteLine("월정보xx: "+Yyyymm +" / "+ Ncount);
                _db.LoadStoredProc("dbo.payment_getPayment").AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId)
                   .AddParam("Dname", LoginUser.Dname).AddParam("Yyyymm", Yyyymm).AddParam("Ncount", int.Parse(Ncount))
                   .Exec(r => plist = r.ToList<PayList>());
            }
            else
            {
                // 월 정보가 있을때
                ViewBag.선택월 = Yyyymm;
                ViewBag.선택회차 = Ncount;
                //Console.WriteLine("월정보있음: "+Yyyymm + " / " + Ncount);
                _db.LoadStoredProc("dbo.payment_lastMonth").AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId)
                   .AddParam("Dname", LoginUser.Dname).Exec(r => monthList = r.ToList<PayList>());
                _db.LoadStoredProc("dbo.payment_getPayment").AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId)
                   .AddParam("Dname", LoginUser.Dname).AddParam("Yyyymm", Yyyymm).AddParam("Ncount", Ncount)
                   .Exec(r => plist = r.ToList<PayList>());
            }

            mymodel.plist = plist;
            mymodel.monthList = monthList;

            int a = 0;
            int b = 0;
            int i = 0;
            foreach (var item in plist)
            {
                item.SSvalue = string.Format("{0:n0}", item.Svalue);
                //Console.WriteLine(item.Slist + "/" + item.SSvalue + "/" + item.Gubun + "/" + item.Fsort + "/  " + i);
                i++;
                if (item.Gubun.Equals("0") && item.Fsort == 0) { a++; }
                if (item.Gubun.Equals("1") && item.Fsort == 0) { b++; }
            }
            //Console.WriteLine(a + " / " + b);
            if (a >= b)
            {
                ViewBag.Crows = a;
            }
            else if (b > a)
            {
                ViewBag.Crows = b;
            }
            
            return View(mymodel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
