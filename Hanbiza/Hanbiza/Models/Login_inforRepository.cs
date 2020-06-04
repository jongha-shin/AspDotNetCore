using Hanbiza.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Hanbiza.Models
{
    public class Login_inforRepository : ILogin_inforRepository
    {
        private readonly ApplicationDbContext dbContext;

        public Login_inforRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public Login_infor GetLogin_Infor(Login_infor login_Infor)
        {
            Console.WriteLine(login_Infor.loginID +" / "+login_Infor.PassW);
            
            Login_infor loginUser = new Login_infor(login_Infor.loginID, login_Infor.PassW);
            return loginUser;
        }
    }
}
