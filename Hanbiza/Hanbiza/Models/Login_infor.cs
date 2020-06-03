using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hanbiza.Models
{
    public class Login_infor
    {
        //SELECT Dname, BizNum, CompanyName, joinday, StaffName, StaffID, loginID, State FROM Login_infor
        [Key]
        public int SEQID { get; set; }
        public string Dname { get; set; }
        public string CompanyName { get; set; }
        public string BizNum { get; set; }
        public string loginID { get; set; }
        public string StaffID { get; set; }
        public string StaffName { get; set; }

    }
}
