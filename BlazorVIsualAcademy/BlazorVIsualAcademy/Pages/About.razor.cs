using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorVIsualAcademy.Pages
{
    public partial class About : ComponentBase
    {
        public string Title = "정보";

        private string subTitle = "사이트 정보";

        public string SubTitle
        {
            get { return subTitle; }
            set { subTitle = value; }
        }

        // 페이지 로드 시 이벤트핸들러
        protected override void OnInitialized() 
        {
            subTitle = DateTime.Now.ToLongTimeString();
        }
    }
}
