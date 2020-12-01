using System;
using System.ComponentModel.DataAnnotations;

namespace HanbizaMVC.Models
{
    public partial class AddTimeList
    {
        [Key]
        public int Seqid { get; set; }
        public string Dname { get; set; }
        public string BizNum { get; set; }
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public string Gubun { get; set; }
        public DateTime Snal { get; set; }
        public DateTime Enal { get; set; }
        public string Reason { get; set; }
        public string HbzMsend { get; set; }
        public DateTime HbzYymmdd { get; set; }
        public int ProCDeep { get; set; }
        public string AllProCess { get; set; }
        public DateTime Regdate { get; set; }

        public string Snals { get; set; }
        public string Enals { get; set; }
        public string 요일 { get; set; }
        public string 년 { get; set; }
        public int DeepNum { get; set; }
    }
}
