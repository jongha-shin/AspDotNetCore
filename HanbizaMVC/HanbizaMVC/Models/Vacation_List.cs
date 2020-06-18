using System;

namespace HanbizaMVC.Models
{
    public class Vacation_List
    {
        public int SEQID { get; set; }
        public string Dname { get; set; }
        public string BizNum { get; set; }
        public int StaffID { get; set; }
        public string Vicname { get; set; }
        public double UseTime { get; set; }
        public DateTime Snal { get; set; }
        public DateTime Enal { get; set; }
        public int ProCDeep { get; set; }
        public string AllProCess { get; set; }
        public string VicReason { get; set; }
        public string HbzMSend { get; set; }
        public DateTime HbzMYYMMDD { get; set; }
        public DateTime Regdate { get; set; }

        public int DeepNum { get; set; }
    }
}
