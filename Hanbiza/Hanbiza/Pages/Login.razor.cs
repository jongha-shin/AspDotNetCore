using Hanbiza.Models;
using Microsoft.AspNetCore.Components;

namespace Hanbiza.Pages
{
    public partial class Login
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        Login_infor1 login_infor = new Login_infor1();
        

        protected override void OnInitialized()
        {
        }

        private void LoginClick()
        {
            System.Console.WriteLine("LoginClick");
            
            login_infor = repository.GetLogin_Infor(login_infor);
            
            // todo : 쿠키저장
            //NavigationManager.NavigateTo("/Main");
        }
    }
}
