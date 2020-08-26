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
        public IActionResult Sub20()
        {
            Boolean checkLogin = CheckLogin();
            if (!checkLogin) return RedirectToAction("Login", "Account");

            ViewBag.menulist = menulist;
            //_logger.LogInformation("sub2(): " + LoginUser.BizNum + " / " + LoginUser.StaffId);

            // OT 신청내역
            List<AddTimeList> OTlist = null;
            _db.LoadStoredProc("dbo.apply_getApplication").AddParam("Type", "OT")
                .AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId).AddParam("Dname", LoginUser.Dname)
                .Exec(r => OTlist = r.ToList<AddTimeList>());

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
        // 20-2 OT 결재 전 삭제
        //[Route("/Apply/Sub20_2/{seqid}")]
        //public string Sub20_2(int seqid)
        //{
        //    //var OTSeq = new AddTimeList { Seqid = seqid };
        //    //_db.Entry(OTSeq).State = EntityState.Deleted;
        //    //int rs = _db.SaveChanges();

        //    //var commandText = "DELETE FROM AddTimeList WHERE SEQID = @seqid";
        //    //var seq = new SqlParameter("@seqid", seqid);
        //    //int rs = _db.Database.ExecuteSqlCommand(commandText, seq);

        //    Boolean checkLogin = CheckLogin();
        //    var rs = _db.LoadStoredProc("dbo.apply_cancel").AddParam("SEQID", seqid).AddParam("Type", "OT")
        //                .ExecNonQuery();

        //    string rsString="error";
        //    if( rs == -1)
        //    {
        //        rsString = "done";
        //        return rsString;
        //    }
        //    if (rs > 0)
        //    {
        //        rsString = "success";
        //        return rsString;
        //    }
        //    return rsString;
        //}

        // 21. 휴가신청
        [Authorize]
        public IActionResult Sub21()
        {
            Boolean checkLogin = CheckLogin();
            if (!checkLogin) return RedirectToAction("Login", "Account");

            ViewBag.menulist = menulist;
            //GetLoginUser();
            //_logger.LogInformation("sub21(): " + LoginUser.BizNum + " / " + LoginUser.StaffId);
            
            List<Vacation_List> Vlist = null;
            _db.LoadStoredProc("dbo.apply_getApplication").AddParam("Type", "vacation")
                .AddParam("BizNum", LoginUser.BizNum).AddParam("StaffId", LoginUser.StaffId).AddParam("Dname", LoginUser.Dname)
                .Exec(r => Vlist = r.ToList<Vacation_List>());

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
            _logger.LogInformation("sub21_1(): " + SearchWord + " / " + Step_num);
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
        // 21-5 휴가결재 진행 전 취소, 등록된 휴가seq 삭제
        [Route("/Apply/Sub21_5/{type}/{seqid}")]
        public string Sub21_5(string type, string seqid)
        {
            Boolean checkLogin = CheckLogin();
            //_logger.LogInformation("sub21_5(): " + LoginUser.BizNum + " / " + LoginUser.StaffId + " / " + seqid);
            string rsString = "fail";

            var rs = _db.LoadStoredProc("dbo.apply_cancel").AddParam("SEQID", seqid).AddParam("Type", type).ExecNonQuery();
            
            if (rs > 0 )
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

        // 기타결재
        [Authorize]
        public IActionResult Sub22()
        {
            Boolean checkLogin = CheckLogin();
            if (!checkLogin) return RedirectToAction("Login", "Account");

            ViewBag.menulist = menulist;
            return View();
        }
        [Authorize]
        [Route("/Apply/Sub22_1/{SearchWord}/{Step_num}/{StaffList}")]
        public IActionResult Sub22_1(string SearchWord, string Step_num, string StaffList)
        {
            Boolean checkLogin = CheckLogin();
            if (!checkLogin) return RedirectToAction("Login", "Account");
            ViewBag.menulist = menulist;

            var jsonString = "";

            if (SearchWord != null)
            {
                if (!SearchWord.Equals(""))
                {
                    List<Approver> Datatable = null;
                    _db.LoadStoredProc("vacation_getApprover").AddParam("Type", "vacation")
                       .AddParam("BizNum", LoginUser.BizNum).AddParam("StaffID", LoginUser.StaffId).AddParam("Dname", LoginUser.Dname)
                       .AddParam("SearchWord", SearchWord).AddParam("Step_num", Step_num).AddParam("StaffList", StaffList)
                       .Exec(r => Datatable = r.ToList<Approver>());

                    jsonString = JsonConvert.SerializeObject(Datatable);
                }
            }
            return new JsonResult(jsonString);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
