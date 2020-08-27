
using System;

namespace HanbizaMVC.Models
{
    public partial class Login_Record
    {
        public int Seqid { get; set; }
        public string DName { get; set; }
        public string BizNum { get; set; }
        public string CompanyName { get; set; }
        public int StaffID { get; set; }
        public DateTime Regdate { get; set; }

    }
}
