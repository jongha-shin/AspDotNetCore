﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HanbizaMVC.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;
using System.Collections.Generic;
using StoredProcedureEFCore;
using System.IO;
using Microsoft.AspNetCore.WebUtilities;

namespace HanbizaMVC.Controllers
{
    public class MyInfoController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public int StaffId;
        public string BizNum;

        public MyInfoController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
// 확인서명
        public IActionResult Sub7()
        {
            StaffId = (int)TempData["StaffId"];
            BizNum = (string)TempData["BizNum"];
            _logger.LogInformation("sub7(): " + BizNum + " / " + StaffId);
            using (var db = new HanbizaContext())
            {
                List<문서함> mySign = null;
                db.LoadStoredProc("dbo.file_getSignature").AddParam("BizNum", BizNum).AddParam("StaffId", StaffId)
                    .Exec(r => mySign = r.ToList<문서함>());

                if (mySign.Count > 0)
                {
                    var aaa = Convert.ToBase64String(mySign[0].FileBlob);
                    //Console.WriteLine("tobase64 : "+aaa);
                    
                    System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                    System.Text.Decoder utf8Decode = encoder.GetDecoder();
                    
                    byte[] todecode_byte = Convert.FromBase64String(aaa);
                    int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                    char[] decoded_char = new char[charCount];
                    utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                    string result = new String(decoded_char);
                    // Console.WriteLine("asdf:: "+result);

                    ViewBag.mySign = result;

                    return View();
                }

            }
                 
            return View();
        }


// 내 문서
        public IActionResult Sub8()
        {
            StaffId = (int)TempData["StaffId"];
            BizNum = (string)TempData["BizNum"];
            _logger.LogInformation("sub8(): " + BizNum + " / " + StaffId);
            using (var db = new HanbizaContext())
            {
                List<문서함> fileList = null;
                db.LoadStoredProc("dbo.filelist").AddParam("BizNum", BizNum).AddParam("StaffId", StaffId)
                    .Exec(r => fileList = r.ToList<문서함>());

                if(fileList.Count > 0)
                {
                    return View(fileList);
                }

            }
                return View();
        }
        public IActionResult Sub9()
        {
            return View();
        }
        public IActionResult LogIn()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(OnlyLogin model)
        {
            _logger.LogInformation("LogInProcess: "+model.LoginID +" / "+ model.passW);
            if (ModelState.IsValid)
            {
                using (var db = new HanbizaContext())
                {
                    //var user1 = from loginUser in db.LoginInfor
                    //           where loginUser.LoginId == model.LoginID
                    //           select  new { loginUser.BizNum, loginUser.StaffId };

                    List<LoginInfor> user = null;
                    db.LoadStoredProc("dbo.loginProcess")
                      .AddParam("loginID", model.LoginID)
                      .AddParam("passW", model.passW)
                      .Exec(r => user = r.ToList<LoginInfor>());
                     
                    // TODO Cookie 저장
                    if(user != null)
                    {
                        foreach (var item in user)
                        {
                            
                            HttpContext.Session.SetString("DateNow", DateTime.Now.ToShortDateString().Substring(0, 7));
                            HttpContext.Session.SetString("BizNum", item.BizNum);
                            HttpContext.Session.SetInt32("StaffId", item.StaffId);
                        }
                        
                        return new RedirectResult("/Home/Index");
                    }
                }
                ModelState.AddModelError(string.Empty, "사용자 ID 혹은 비밀번호가 올바르지 않습니다.");
            }
             
            return View(model);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
