namespace Hanbiza.Models
{
    public interface ILogin_inforRepository
    {
        LoginInfor GetLogin_Infor(LoginInfor login_Infor);

        void SaveCookie(LoginInfor login_Infor);

    }
}
