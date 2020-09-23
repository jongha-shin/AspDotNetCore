﻿using System;

namespace HanbizaMVC.ViewModel
{
    public class ApproveList
    {
        public int VacID { get; set; }
            public int OtID { get; set; }           // OT
                public int EtcID { get; set; }           // ETC
        public string AResult { get; set; }
        public string processPoint { get; set; }
        public string RereaSon { get; set; }
        public int StaffID { get; set; }
        public DateTime SNal { get; set; }
        public DateTime ENal { get; set; }
        public string Vicname { get; set; }
            public string Gubun { get; set; }       // OT
                public string Gubun1 { get; set; }       // ETC
                public string Gubun2 { get; set; }       // ETC
        public double UseTime { get; set; }
        public string Staffname { get; set; }
        public string VicReaSon { get; set; }
            public string Reason { get; set; }      // OT
                public string EtcReason { get; set; }       // ETC
        public string AllProCess { get; set; }
        public double 잔여일수 { get; set; }
    }
}
