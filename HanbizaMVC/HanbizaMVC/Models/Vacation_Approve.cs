using System;

namespace HanbizaMVC.Models
{
    public class Vacation_Approve
    {
        public int SEQID { get; set; }
        public string Dname { get; set; }
        public string BizNum { get; set; }
        public int VacID { get; set; }
        public int approveID { get; set; }
        public string approveName { get; set; }
        public int DeepNum { get; set; }
        public string processPoint { get; set; }
        public string RereaSon { get; set; }
        public string AResult { get; set; }
        public DateTime Regdate { get; set; }
    }
}
