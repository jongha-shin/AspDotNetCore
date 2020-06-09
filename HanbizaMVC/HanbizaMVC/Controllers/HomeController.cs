﻿using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HanbizaMVC.Models;
using System;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using StoredProcedureEFCore;
using System.Collections.Generic;
using HanbizaMVC.ViewModel;

namespace HanbizaMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Sub1()
        {
            var oBizNum = TempData["BizNum"];
            var oStaffId = TempData["StaffId"];
            var DateNow = TempData["DateNow"];

            string BizNum = (string)oBizNum;
            int StaffId = (int)oStaffId;
            _logger.LogInformation(BizNum + " / " + StaffId + " / " + DateNow);
            using (var db = new HanbizaContext()) {
                
                // 최근 근태기록 월 구하기
                db.LoadStoredProc("dbo.lastMonth").AddParam("BizNum", BizNum).AddParam("StaffId", StaffId)
                    .ExecScalar(out string dateMonth);

                ViewBag.최근월 = dateMonth;

                // 월별근태내역 - 근무/휴가
                var CulTable = from data in db.출퇴근기록집계표
                               where data.StaffId == StaffId
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
                db.LoadStoredProc("dbo.totalAttendence").AddParam("BizNum", BizNum).AddParam("StaffId", StaffId).AddParam("lastMonth", dateMonth)
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
                db.LoadStoredProc("dbo.attendRecord").AddParam("BizNum", BizNum).AddParam("StaffId", StaffId).AddParam("lastMonth", dateMonth)
                    .Exec(r => recordTable = r.ToList<출퇴근기록>());

                ViewBag.recordTable = recordTable;

                if (CulTable != null)
                {
                    return View(recordTable);
                }

            }
            return View();
        }
        public IActionResult Sub2()
        {
            return View();
        }
        public IActionResult Sub3()
        {
            return View();
        }
        public IActionResult Sub4()
        {
            return View();
        }
        public IActionResult Sub5()
        {
            return View();
        }
        public IActionResult Sub6()
        {
            return View();
        }
        
      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
