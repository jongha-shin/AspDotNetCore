using Hanbiza.Data;
using Hanbiza.Pages;
using System.Collections.Generic;
using System.Linq;


namespace Hanbiza.Models
{
    public class Login_inforRepository : ILogin_inforRepository
    {
        private readonly ApplicationDbContext dbContext;

        public Login_inforRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public Login_infor1 GetLogin_Infor(Login_infor1 login_infor)
        {
            System.Console.WriteLine("GetLogin_Infor(): "+ login_infor.loginID +" / "+ login_infor.PassW);
            var loginUser = from user in dbContext.Login_infor
                            where user.loginID == login_infor.loginID
                            select user;
            
            //IEnumerable<Login_infor1> loginUser = from user in dbContext.Login_infor
            //                                      where user.loginID == login_infor.loginID
            //                                          && user.PassW == login_infor.PassW
            //                                      select new Login_infor1
            //                                      {
            //                                          Dname = user.Dname,
            //                                          BizNum = user.BizNum,
            //                                          CompanyName = user.CompanyName,
            //                                          loginID = user.loginID,
            //                                          StaffID = user.StaffID,
            //                                          StaffName = user.StaffName
            //                                      };

            foreach (var item in loginUser)
            {
                System.Console.WriteLine(item.CompanyName);
            }
            

            
            return (Login_infor1)loginUser;
        }
    }
}
