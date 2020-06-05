using Hanbiza.Data;
using Hanbiza.Pages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Collections.Generic;
using System.Linq;


namespace Hanbiza.Models
{
    public class Login_inforRepository : ILogin_inforRepository
    {
        private readonly MyDbContext dbContext;

        public Login_inforRepository(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public LoginInfor GetLogin_Infor(LoginInfor login_infor)
        {
            System.Console.WriteLine("GetLogin_Infor(): "+ login_infor.LoginId + " / "+ login_infor.PassW);
            var loginUser = from user in dbContext.LoginInfor
                            where user.LoginId == login_infor.LoginId
                            select new 
                            {   
                                user.Dname, 
                                user.BizNum, 
                                user.CompanyName, 
                                user.LoginId, 
                                user.StaffId, 
                                user.StaffName
                            };
            
            //var loginUser = dbContext.LoginInfor.FromSqlRaw("SELECT SEQID, Dname, BizNum, CompanyName, StaffName, StaffID, loginID, JoinDay, State, Regdate, Bigo, BBuseo, MBuseo, Buseo FROM Login_infor WHERE LoginId = '01011111111'").ToList();
            //var loginUser = dbContext.LoginInfor.FromSqlRaw("SELECT * FROM Login_infor WHERE (LoginId = '" + login_infor.LoginId + "') AND PwdCompare('" + login_infor.PassW + "', PassW) = 1").ToList();

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
                System.Console.WriteLine(item.ToString());
            }
            
            return (LoginInfor)loginUser;
        }

        public void SaveCookie(LoginInfor login_infor)
        {

        }
    }
}
