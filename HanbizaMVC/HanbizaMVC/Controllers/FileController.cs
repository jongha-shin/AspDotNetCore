using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HanbizaMVC.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using StoredProcedureEFCore;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Configuration;

namespace HanbizaMVC.Controllers
{
    public class FileController : Controller
    {
        private readonly ILogger<FileController> _logger;
        private readonly IConfiguration _configuration;

        public FileController(ILogger<FileController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public FileContentResult FileDownload(int id)
        {
            //_logger.LogInformation("FileDownload() " + id);
            using (var db = new HanbizaContext(_configuration))
            {

                List<문서함> fileInfo = null;
                db.LoadStoredProc("file_data").AddParam("SeqID", id).Exec(r => fileInfo = r.ToList<문서함>());
                //_logger.LogInformation("FileDownload() " + fileInfo[0].FileName);

                var fileRes = new FileContentResult(fileInfo[0].FileBlob.ToArray(), "application/octet-stream")
                {
                    FileDownloadName = fileInfo[0].FileName
                };

                //List<GB_INT_GET_APPR_FILE_INFO_Result> file = db2.GB_INT_GET_APPR_FILE_INFO(appr_no, file_seq).ToList();
                //var fileRes = new FileContentResult(file[0].file_attach.ToArray(), "application/octet-stream");
                //fileRes.FileDownloadName = file[0].file_name;
                  
                return fileRes;

                //byte[] fileBytes = System.IO.File.ReadAllBytes(
                //    Path.Combine(
                //        _environment.WebRootPath, "files") + "\\" + fileName);
            
                //return File(fileBytes, "application/octet-stream", fileName);
            }
        }
        [HttpPost]
        public string SaveSign(string imageData, string SEQID)
        {

            if (SEQID == null) SEQID = "0";
            int SeqID = Int32.Parse(SEQID);
            _logger.LogInformation("SaveSign()1 " + imageData + "\n seqid: " + SEQID);

            string BizNum = HttpContext.Session.GetString("BizNum");
            int StaffID = (int)HttpContext.Session.GetInt32("StaffId");
            string Base64string = imageData;
            string ImageFileName = "sign_" + StaffID + ".jpg";
            string ImageDir = "FileBox/" + BizNum;
            string StaffName = HttpContext.Session.GetString("StaffName");
            string Dname = HttpContext.Session.GetString("Dname");

            using (var db = new HanbizaContext(_configuration))
            {
                var rs = 
                    db.LoadStoredProc("file_SaveSignature").AddParam("BizNum", BizNum).AddParam("StaffID", StaffID).AddParam("Base64string", Base64string)
                    .AddParam("ImageFileName", ImageFileName).AddParam("ImageDir", ImageDir).AddParam("StaffName", StaffName).AddParam("Dname", Dname).AddParam("SEQID", SeqID)
                    .ExecNonQuery();

                if (rs > 0) return "success";
                
            }

                return "fail";
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
