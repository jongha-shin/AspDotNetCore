using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HanbizaMVC.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using StoredProcedureEFCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;

namespace HanbizaMVC.Controllers
{
    public class FileController : Controller
    {
        private readonly ILogger<FileController> _logger;
        private IWebHostEnvironment _environment;

        public FileController(ILogger<FileController> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }
        public FileContentResult FileDownload(int id)
        {
            //_logger.LogInformation("FileDownload() " + id);
            using (var db = new HanbizaContext())
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
