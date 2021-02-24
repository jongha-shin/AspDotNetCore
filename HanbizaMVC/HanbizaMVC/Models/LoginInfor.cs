using System;
using System.Collections.Generic;

namespace HanbizaMVC.Models
{
    public partial class LoginInfor
    {
        public int Seqid { get; set; }
        public string Dname { get; set; }
        public string BizNum { get; set; }
        public string CompanyName { get; set; }
        public string StaffName { get; set; }
        public int StaffId { get; set; }
        public string LoginId { get; set; }
        public byte[] PassW { get; set; }
        public DateTime? JoinDay { get; set; }
        public string State { get; set; }
        public DateTime? Regdate { get; set; }
        public string Bigo { get; set; }
        public string Bbuseo { get; set; }
        public string Mbuseo { get; set; }
        public string Buseo { get; set; }
        public string SMSuse { get; set; }
        public string SMStime { get; set; }
    }
}
