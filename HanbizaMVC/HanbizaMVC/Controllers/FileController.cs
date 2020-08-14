using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HanbizaMVC.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using StoredProcedureEFCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace HanbizaMVC.Controllers
{
    public class FileController : Controller
    {
        private readonly ILogger<FileController> _logger;
        private readonly HanbizaContext _db;
        private LoginInfor LoginUser;
        //static public string ToReadableByteArray(byte[] bytes)
        //{
        //    return string.Join(", ", bytes);
        //}
        public FileController(ILogger<FileController> logger, HanbizaContext db)
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
                return true;
            }
            else
            {
                return false;
            }
        }

        public FileContentResult FileDownload(int id)
        {
            //_logger.LogInformation("FileDownload() :" + id);

            List<문서함> fileInfo = null;
            _db.LoadStoredProc("file_data").AddParam("SeqID", id).Exec(r => fileInfo = r.ToList<문서함>());
            //_logger.LogInformation("FileDownload() " + fileInfo[0].FileName);

            var fileRes = new FileContentResult(fileInfo[0].FileBlob.ToArray(), "application/octet-stream")
            {
                FileDownloadName = fileInfo[0].FileName
            };
            return fileRes;
        }
        [HttpPost]
        public async Task<IActionResult> SaveSign()
        {
            Boolean checkLogin = CheckLogin();
            if (!checkLogin) return RedirectToAction("Login", "Account");

            var form = await Request.ReadFormAsync();
            var file = form.Files.First();
            //var bytes = /*file.OpenReadStream().GetType();*/ file.OpenReadStream();

            //_logger.LogInformation("SaveSign()1 :" + bytes);
            var fileBytes = new byte[file.OpenReadStream().Length];
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();

                // act on the Base64 data
            }
            int SeqID = 0;

            string BizNum = LoginUser.BizNum;
            int StaffID = LoginUser.StaffId;
            //string ImageFileName = "sign_" + StaffID + ".jpeg";
            string ImageFileName = "sign_" + StaffID + ".png";
            string ImageDir = "FileBox/" + BizNum;

            var rs =
                _db.LoadStoredProc("file_SaveSignature").AddParam("BizNum", LoginUser.BizNum).AddParam("StaffID", LoginUser.StaffId)
                    .AddParam("Base64string", fileBytes).AddParam("ImageFileName", ImageFileName).AddParam("ImageDir", ImageDir)
                    .AddParam("StaffName", LoginUser.StaffName).AddParam("Dname", LoginUser.Dname).AddParam("SEQID", SeqID)
                    .ExecNonQuery();

            string reponse = "fail";
            if (rs > 0) reponse = "success";
            return Json(reponse);

        }

        [HttpPost]
        [Route("/File/SavePngWithSign/{Seqid}")]
        public async Task<IActionResult> SavePngWithSign(int Seqid)
        {
            Boolean checkLogin = CheckLogin();
            if (!checkLogin) return RedirectToAction("Login", "Account");

            var form = await Request.ReadFormAsync();
            var file = form.Files.First();
            //var bytes = /*file.OpenReadStream().GetType();*/ file.OpenReadStream();

            _logger.LogInformation("SaveSign()1 :" + Seqid);
            var fileBytes = new byte[file.OpenReadStream().Length];
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();

            }
            int SeqID = Seqid;
            
            var rs = _db.LoadStoredProc("file_SavePngWithSign").AddParam("Base64string", fileBytes).AddParam("SEQID", SeqID)
                        .ExecNonQuery();
            string reponse = "fail";
            if (rs > 0) reponse = "success";
            return Json(reponse);

        }
        [HttpPost]
        [Route("/File/SavePdfWithSign/{Seqid}")]
        public async Task<IActionResult> SavePdfWithSign(int Seqid)
        {
            Boolean checkLogin = CheckLogin();
            if (!checkLogin) return RedirectToAction("Login", "Account");

            var form = await Request.ReadFormAsync();
            var file = form.Files.First();
            //var bytes = /*file.OpenReadStream().GetType();*/ file.OpenReadStream();

            _logger.LogInformation("SaveSign()1 :" + Seqid);
            var fileBytes = new byte[file.OpenReadStream().Length];
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();

            }
            int SeqID = Seqid;
            int FSize = fileBytes.Length;
            var rs = _db.LoadStoredProc("file_SavePdfWithSign").AddParam("Base64string", fileBytes).AddParam("SEQID", SeqID).AddParam("FSize", FSize)
                        .ExecNonQuery();
            string reponse = "fail";
            if (rs > 0) reponse = "success";
            return Json(reponse);

        }
        
        public FileContentResult SignDownload(int id)
        {
            //Console.WriteLine("sign down: seq ="+id);
            List<문서함> mySign = null;
            _db.LoadStoredProc("file_data").AddParam("SeqID", id).Exec(r => mySign = r.ToList<문서함>());

            //Console.WriteLine("byte[] : " + mySign[0].FileBlob.ToArray());

            var fileRes = new FileContentResult(mySign[0].FileBlob.ToArray(), "application/octet-stream")
            {
                FileDownloadName = mySign[0].FileName
            };
            return fileRes;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
