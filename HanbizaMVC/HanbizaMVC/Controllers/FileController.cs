using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HanbizaMVC.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.EntityFrameworkCore;
using StoredProcedureEFCore;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace HanbizaMVC.Controllers
{
    public class FileController : Controller
    {
        private readonly ILogger<FileController> _logger;
        private IWebHostEnvironment _environment;

        public FileController(ILogger<FileController> logger)
        {
            _logger = logger;
        }
        public FileController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        // 파일이름 수정
        public FileResult FileDownload(string fileName = "test.txt")
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(
                Path.Combine(
                    _environment.WebRootPath, "files") + "\\" + fileName);

            return File(fileBytes, "application/octet-stream", fileName);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
