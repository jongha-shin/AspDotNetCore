using Hanbiza.Data;
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
            IEnumerable<Login_infor1> loginUser = (IEnumerable<Login_infor1>)(from user in dbContext.Login_infor
                                                 where user.loginID == login_infor.loginID
                                                       && user.PassW == login_infor.PassW
                                                 select new
                                                 {
                                                     user.BizNum, user.CompanyName, user.Dname, 
                                                     user.loginID, user.StaffID, user.StaffName
                                                 });
            asdf.DataSource = loginUser;

            
            return (Login_infor1)loginUser;
        }
    }
}
