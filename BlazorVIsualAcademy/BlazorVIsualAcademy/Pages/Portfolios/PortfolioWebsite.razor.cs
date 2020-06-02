using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorVIsualAcademy.Pages.Portfolios
{
    public partial class PortfolioWebsite
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }


        protected override void OnInitialized()
        {
            
        }

        protected void GoToDotNetKorea()
        {
            //NavigationManager.NavigateTo("https://www.naver.com");
            NavigationManager.NavigateTo("/Index");
        }

    }
}
