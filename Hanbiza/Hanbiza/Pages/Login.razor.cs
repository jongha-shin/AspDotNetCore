using Hanbiza.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;

namespace Hanbiza.Pages
{
    public partial class Login
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public ILogin_inforRepository repository;
        
        Login_infor login_infor = new Login_infor();

        protected override void OnInitialized()
        {
           
        }

        public void Login_Click(string ID, string passW)
        {
            login_infor.loginID = ID;
            login_infor.PassW = passW;
            var loginUser = repository.GetLogin_Infor(login_infor);
            
            // todo : 쿠키저장
            NavigationManager.NavigateTo("/Main");
        }
    }
}
