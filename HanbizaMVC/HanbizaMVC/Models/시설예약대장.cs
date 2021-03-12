using System;

namespace HanbizaMVC.Models
{
    public partial class 시설예약대장
    {
        public int SeqID { get; set; }
        public string Dname { get; set; }
        public string BizNum { get; set; }
        public string Gubun1 { get; set; }
        public string Gubun2 { get; set; }
        public DateTime Start_Time { get; set; }
        public DateTime End_Time { get; set; }
        public string 예약인 { get; set; }
        public DateTime? Regdate { get; set; }
        
    }
}
