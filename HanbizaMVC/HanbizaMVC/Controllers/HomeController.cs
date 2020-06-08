using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HanbizaMVC.Models;
using System;

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
            var BizNum = TempData["BizNum"];
            var oStaffId = TempData["StaffId"];
            int StaffId = (int)oStaffId;
            _logger.LogInformation(BizNum + "/" + StaffId + " / " + DateTime.Now);
            using (var db = new HanbizaContext()) {
                var CulTable = from data in db.출퇴근기록집계표
                               where data.StaffId == StaffId
                               select data;
                //{
                //    기준일 = data.기준일,
                //    근무일 = data.근무일,
                //    결근일 = data.결근일,
                //    휴무일 = data.휴무일,
                //    주휴일 = data.주휴일,
                //    유급휴일 = data.유급휴가휴일 + data.유급휴일,
                //    무급휴가휴일 = data.무급휴가휴일,
                //    유급주휴일 = data.유급주휴일
                //};

                //CulTable = db.출퇴근기록집계표.Where(u => u.StaffId.Equals(StaffId)).FirstOrDefault();

                foreach (var i in CulTable)
                {
                    System.Console.WriteLine(i.근무일);
                    ViewBag.기준일 = i.기준일;
                    ViewBag.근무일 = i.근무일;
                    ViewBag.결근일 = i.결근일;
                    ViewBag.휴무일 = i.휴무일;
                    ViewBag.주휴일 = i.주휴일;
                    ViewBag.유급휴일 = i.유급휴가휴일 + i.유급휴일;
                    ViewBag.무급휴가휴일 = i.무급휴가휴일;
                    ViewBag.유급주휴일 = i.유급주휴일;
                }

                if(CulTable != null)
                {
                    return View();
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
