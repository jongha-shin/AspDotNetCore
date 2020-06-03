using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hanbiza.Pages
{
    public partial class Login
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected override void OnInitialized()
        {
            
        }

        protected void Login_Click()
        {
            NavigationManager.NavigateTo("/Main");
        }
    }
}
