using Hanbiza.Data;
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
            var loginUser = from Login_infor in dbContext.Login_infor
                            where Login_infor.loginID == login_infor.loginID 
                            && Login_infor.PassW == login_infor.PassW
                            select Login_infor;
            
            
            return login_infor;
        }
    }
}
