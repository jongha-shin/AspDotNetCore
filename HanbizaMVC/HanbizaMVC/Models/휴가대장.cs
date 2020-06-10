using System;
using System.Collections.Generic;

namespace HanbizaMVC.Models
{
    public partial class 휴가대장
    {
        public int Seqid { get; set; }
        public string Dname { get; set; }
        public string BizNum { get; set; }
        public int StaffId { get; set; }
        public DateTime 날짜 { get; set; }
        public double? 반차 { get; set; }
        public string 등록인 { get; set; }
        public DateTime? Regdate { get; set; }
        public int 번호 { get; set; }
    }
}
