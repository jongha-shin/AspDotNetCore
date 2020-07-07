using System.ComponentModel.DataAnnotations;

namespace HanbizaMVC.Models
{
    public class OnlyLogin
    {
        [Required(ErrorMessage = "사용자 ID를 입력하세요.")]
        public string LoginID { get; set; }

        [Required(ErrorMessage = "사용자 비밀번호를 입력하세요.")]
        public string passW { get; set; }

        public string Auto_save { get; set; }
    }
}
