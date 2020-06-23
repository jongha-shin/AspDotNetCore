using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HanbizaMVC.Controllers
{
    public class AuthenticationController : Controller
    {
        public async Task<IActionResult> Login()
        {
            // db에서 비교해서 로그인 정보 가져오기

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "UserID"),
                new Claim(ClaimTypes.Name, "UserName"),
                new Claim(ClaimTypes.Email, "Email"),
                new Claim(ClaimTypes.Role, "Role")
                // 등등
            };
            // claims, "Cookies"
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme); 

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal,
                new AuthenticationProperties { IsPersistent = false});

            return Content("로그인되었습니다.");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            return Content("로그아웃되었습니다");
        }


        // 로그인 정보 확인
        public IActionResult LoginPartial()
        {
            string result = "";
            if (User.Identity.IsAuthenticated)
            {
                result = $"<h3>{User.Identity.Name}</h3>";
                foreach (var claim in User.Claims)
                {
                    result += $"{claim.Type} - {claim.Value}<br />";
                }
            }
            else
            {
                result = "로그인하지 않았습니다.";
            }
            return Content(result, "text/html", Encoding.Default);
        }

    }
}
