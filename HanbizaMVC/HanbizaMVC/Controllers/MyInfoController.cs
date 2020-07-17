using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HanbizaMVC.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;
using System.Collections.Generic;
using StoredProcedureEFCore;
using Microsoft.AspNetCore.Authorization;

namespace HanbizaMVC.Controllers
{
    public class MyInfoController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HanbizaContext _db;
        private readonly LoginInfor LoginUser = AccountController.LoginUser;
        private readonly List<회사별메뉴> menulist = AccountController.menulist;

        public MyInfoController(ILogger<HomeController> logger, HanbizaContext db)
        {
            _logger = logger;
            _db = db;
        }

        // 확인서명
        [Authorize]
        public IActionResult Sub7()
        {
            ViewBag.menulist = menulist;
            //_logger.LogInformation("sub7(): " + LoginUser.BizNum + " / " + LoginUser.StaffId);

            List<문서함> mySign = null;
            _db.LoadStoredProc("dbo.file_getSignature").AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId)
                .Exec(r => mySign = r.ToList<문서함>());

            if (mySign.Count > 0)
            {
                if(mySign[0].Regdate > Convert.ToDateTime("2020-07-15"))
                {
                    var stringify_byte = Convert.ToBase64String(mySign[0].FileBlob);
                    Console.WriteLine("tobase64 : " + stringify_byte);
                    string result = "data:image/png;base64," + stringify_byte;
                    ViewBag.mySign = result;
                }
                else
                {
                    var stringify_byte = Convert.ToBase64String(mySign[0].FileBlob);
                    System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                    System.Text.Decoder utf8Decode = encoder.GetDecoder();
                    byte[] todecode_byte = Convert.FromBase64String(stringify_byte);
                    Console.WriteLine("byte: " + todecode_byte);
                    int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                    char[] decoded_char = new char[charCount];
                    utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                    string result = new String(decoded_char);
                    Console.WriteLine("result: " + result);
                    ViewBag.mySign = result;
                }
                ViewBag.SEQID = mySign[0].SeqId;

                return View();
            }

            return View();
        }


        // 내 문서
        [Authorize]
        public IActionResult Sub8()
        {
            ViewBag.menulist = menulist;
            //_logger.LogInformation("sub8(): " + LoginUser.BizNum + " / " + LoginUser.StaffId);

            List<문서함> fileList = null;
            _db.LoadStoredProc("dbo.filelist").AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId)
                .Exec(r => fileList = r.ToList<문서함>());

            if (fileList.Count > 0)
            {
                return View(fileList);
            }

            return View();
        }
        [Authorize]
        public IActionResult Sub9()
        {
            ViewBag.menulist = menulist;
            return View();
        }
       
        //[HttpPost]
        //public IActionResult LogIn(OnlyLogin model)
        //{
        //    _logger.LogInformation("LogInProcess: " + model.LoginID + " / " + model.passW);
        //    if (ModelState.IsValid)
        //    {
        //        using (var db = new HanbizaContext())
        //        {
        //            //var user1 = from loginUser in db.LoginInfor
        //            //           where loginUser.LoginId == model.LoginID
        //            //           select  new { loginUser.BizNum, loginUser.StaffId };

        //            List<LoginInfor> user = null;
        //            db.LoadStoredProc("dbo.loginProcess")
        //              .AddParam("loginID", model.LoginID)
        //              .AddParam("passW", model.passW)
        //              .Exec(r => user = r.ToList<LoginInfor>());

        //            // TODO Cookie 저장
        //            if (user != null)
        //            {
        //                foreach (var item in user)
        //                {
        //                    HttpContext.Session.SetString("DateNow", DateTime.Now.ToShortDateString().Substring(0, 7));
        //                    HttpContext.Session.SetString("BizNum", item.BizNum);
        //                    HttpContext.Session.SetInt32("StaffId", item.StaffId);
        //                    HttpContext.Session.SetString("StaffName", item.StaffName);
        //                    HttpContext.Session.SetString("Dname", item.Dname);
        //                }

        //                return new RedirectResult("/Home/Index");
        //            }
        //        }
        //        ModelState.AddModelError(string.Empty, "사용자 ID 혹은 비밀번호가 올바르지 않습니다.");
        //    }

        //    return View(model);
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
