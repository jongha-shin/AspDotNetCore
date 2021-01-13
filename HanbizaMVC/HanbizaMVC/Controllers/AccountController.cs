using HanbizaMVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoredProcedureEFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Net;

namespace HanbizaMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly HanbizaContext _db;
        public static LoginInfor LoginUser;
        public static List<회사별메뉴> menulist;

        public AccountController(HanbizaContext db)
        {
            _db = db;
        }
        public IActionResult Login()
        {
            // 쿠키 체크, 정보 가져오기
            //Console.WriteLine("로그인 화면");
            if (User.Identity.IsAuthenticated)
            {
                string LoginId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var loginInfo = from info in _db.LoginInfor
                              where info.LoginId == LoginId
                              select info;

                foreach (var item in loginInfo)
                {
                    ViewBag.loginID = item.LoginId;
                    // pwd 저장시 여기서 가져와서 로그인 화면으로
                } 
            }


            //For IpAddress
            //IPHostEntry iphostinfo = Dns.GetHostEntry(Dns.GetHostName());
            //string IpAddress = Convert.ToString(iphostinfo.AddressList.FirstOrDefault(address => address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork));

            ////For MacID
            //ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            //ManagementObjectCollection moc = mc.GetInstances();
            //string MACAddress = string.Empty;
            //foreach(ManagementObject mo in moc)
            //{
            //    if(MACAddress == string.Empty)
            //    {
            //        if ((bool)mo["IPEnabled"] == true) MACAddress = mo["MacAddress"].ToString();
            //    }
            //    mo.Dispose();
            //}
            //MACAddress = MACAddress.Replace(":", "-");

            //add in db
            //Console.WriteLine("1: " + IpAddress);
            //-----------------------------------------------------------------------------------------------
            //IPAddress ip;
            //var headers = Request.Headers.ToList();
            //if (headers.Exists((kvp) => kvp.Key == "X-Forwarded-For"))
            //{
            //    // when running behind a load balancer you can expect this header
            //    var header = headers.First((kvp) => kvp.Key == "X-Forwarded-For").Value.ToString();
            //    ip = IPAddress.Parse(header);
            //}
            //else
            //{
            //    // this will always have a value (running locally in development won't have the header)
            //    ip = Request.HttpContext.Connection.RemoteIpAddress;
            //}
            //Console.WriteLine("2: " + ip);



            return View();

        }
        [Route("/Account/Login/{userID}/{userPWD}/{autoSave}")]
        public string Login(string userID, string userPWD, string autoSave)
        {
            //Console.WriteLine("login() autoSave: "+ autoSave);
            LoginInfor _LoginUser = new LoginInfor();
            _db.LoadStoredProc("dbo.login_Process").AddParam("loginID", userID).AddParam("passW", userPWD)
              .Exec(r => _LoginUser = r.SingleOrDefault<LoginInfor>());
            LoginUser = _LoginUser;
            string rs;
            if (LoginUser != null)
            {
                menulist = _db.회사별메뉴.Where(r => r.BizNum == LoginUser.BizNum && r.DName == LoginUser.Dname).ToList();
                var claims = BuildClaims(LoginUser);
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                if (autoSave.Equals("not_save"))
                {
                    //Console.WriteLine("------auto_save NONONONONO------");
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal,
                    new AuthenticationProperties { IsPersistent = false });
                }
                else
                {
                    //Console.WriteLine("------auto_save------");
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal,
                         new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTime.UtcNow.AddDays(50) });
                        //new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTime.UtcNow.AddSeconds(10)});
                }

                IPAddress ip;
                var headers = Request.Headers.ToList();
                if (headers.Exists((kvp) => kvp.Key == "X-Forwarded-For"))
                {
                    // when running behind a load balancer you can expect this header
                    var header = headers.First((kvp) => kvp.Key == "X-Forwarded-For").Value.ToString();
                    ip = IPAddress.Parse(header);
                }
                else
                {
                    // this will always have a value (running locally in development won't have the header)
                    ip = Request.HttpContext.Connection.RemoteIpAddress;
                }
                


                // 로그인 기록 남기기
                int a = _db.LoadStoredProc("dbo.login_insert_Record_IP").AddParam("Dname", LoginUser.Dname).AddParam("BizNum", LoginUser.BizNum)
                           .AddParam("CompanyName", LoginUser.CompanyName).AddParam("StaffID", LoginUser.StaffId).AddParam("IP", ip.ToString())
                           .ExecNonQuery();
                if (a <= 0) return rs = "fail";

                rs = "success";
            }
            else
            {
                rs = "fail";
            }
            return rs;
        }

        private IList<Claim> BuildClaims(LoginInfor account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, $"{account.LoginId}"),
                //new Claim("StaffName", account.StaffName),
                //new Claim("Dname", account.Dname),
                //new Claim("BizNum", account.BizNum),
                //new Claim("StaffID", $"{account.StaffId}"),
                //new Claim("DateNow",  DateTime.Now.ToShortDateString().Substring(0, 7)),
            };
            return claims;
        }

        [Route("/Account/AdminLogin/{Dname}/{BizNum}/{StaffID}")]
        public string AdminLogin(string Dname, string BizNum, string StaffID)
        {
            //Console.WriteLine("login() autoSave: "+ autoSave);
            LoginInfor _LoginUser = new LoginInfor();
            _db.LoadStoredProc("dbo.login_Admin_Process").AddParam("Dname", Dname).AddParam("BizNum", BizNum).AddParam("StaffID", StaffID)
              .Exec(r => _LoginUser = r.SingleOrDefault<LoginInfor>());
            LoginUser = _LoginUser;
            string rs;
            if (LoginUser != null)
            {
                menulist = _db.회사별메뉴.Where(r => r.BizNum == LoginUser.BizNum && r.DName == LoginUser.Dname).ToList();
                var claims = BuildClaims(LoginUser);
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal,
                    new AuthenticationProperties { IsPersistent = false });
                rs = "success";
            }
            else
            {
                rs = "fail";
            }
            return rs;
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");

            return RedirectToAction("LogIn", "Account");
        }


        public ActionResult SideMenu()
        {
            return PartialView("SideMenu");
        }
    }
}
