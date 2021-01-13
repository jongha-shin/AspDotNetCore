using System;

namespace HanbizaMVC.Models
{
    public class WorkTime
    {
        public int SEQID { get; set; }
        public string Dname { get; set; }
        public string BizNum { get; set; }
        public int PkDay { get; set; }
        public int MatchNum { get; set; }
        public string Sabun { get; set; }
        public string BuSeo { get; set; }
        public string StaffName { get; set; }
        public string RecognizeNum {get; set;}
        public string StartWork { get; set; }
        public string EndWork { get; set; }
        public string HbzUp { get; set; }
        public string Bigo { get; set; }
        public DateTime Regdate { get; set; }
    }
}
