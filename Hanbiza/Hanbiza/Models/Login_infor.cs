using System.ComponentModel.DataAnnotations;

namespace Hanbiza.Models
{
    public class Login_infor
    {
        //SELECT Dname, BizNum, CompanyName, joinday, StaffName, StaffID, loginID, State FROM Login_infor
        [Key]
        public int SEQID { get; set; }
        public string Dname { get; set; }
        public string BizNum { get; set; }
        public string CompanyName { get; set; }
        public string loginID { get; set; }
        public string PassW { get; set; }
        public string StaffID { get; set; }
        public string StaffName { get; set; }

        public Login_infor(int sEQID, string dname, string bizNum, string companyName, string loginID, string passW, string staffID, string staffName)
        {
            SEQID = sEQID;
            Dname = dname;
            BizNum = bizNum;
            CompanyName = companyName;
            this.loginID = loginID;
            PassW = passW;
            StaffID = staffID;
            StaffName = staffName;
        }

        public Login_infor(string loginID, string passW)
        {
            this.loginID = loginID;
            PassW = passW;
        }

        public Login_infor()
        {
        }
    }

}
