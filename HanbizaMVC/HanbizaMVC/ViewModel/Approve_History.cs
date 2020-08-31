using System;

namespace HanbizaMVC.ViewModel
{
    public class Approve_History
    {
        public string Type { get; set; }
        public int SEQID { get; set; }
            public int OtID { get; set; }           //OT
        public string Dname { get; set; }
        public string BizNum { get; set; }
        public int approveID { get; set; }
        public string approveName { get; set; }
        public int DeepNum { get; set; }
        public string processPoint { get; set; }
        public string RereaSon { get; set; }
        public string AResult { get; set; }
        public string Regdate { get; set; }
        public DateTime SNal { get; set; }
        public DateTime Enal { get; set; }
        public int StaffID { get; set; }
        public string Vicname { get; set; }
        public double UseTime { get; set; }
        public string StaffName { get; set; }
           
            public string Gubun { get; set; }       // OT

    }
}
