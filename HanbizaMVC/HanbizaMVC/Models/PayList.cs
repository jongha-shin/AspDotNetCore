using System;
using System.Collections.Generic;

namespace HanbizaMVC.Models
{
    public partial class PayList
    {
        public int Seqid { get; set; }
        public string Dname { get; set; }
        public string BizNum { get; set; }
        public int StaffId { get; set; }
        public string Yyyymm { get; set; }
        public string Gubun { get; set; }
        public string Slist { get; set; }
        public double? Svalue { get; set; }
        public int? Ncount { get; set; }
        public string 등록인 { get; set; }
        public DateTime? Regdate { get; set; }
        public int? Fsort { get; set; }
    }
}
