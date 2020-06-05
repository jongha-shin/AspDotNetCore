using System;
using System.Collections.Generic;

namespace Hanbiza.Models
{
    public partial class 출퇴근기록집계표
    {
        public int Seqid { get; set; }
        public string Dname { get; set; }
        public string BizNum { get; set; }
        public int StaffId { get; set; }
        public DateTime 시작일 { get; set; }
        public DateTime 종료일 { get; set; }
        public int? 기준일 { get; set; }
        public double? 휴무일 { get; set; }
        public double? 유급휴일 { get; set; }
        public double? 근무일 { get; set; }
        public double? 유급휴가휴일 { get; set; }
        public double? 무급휴가휴일 { get; set; }
        public double? 결근일 { get; set; }
        public double? 주휴일 { get; set; }
        public double? 유급주휴일 { get; set; }
        public string 등록인 { get; set; }
        public DateTime? Regdate { get; set; }
    }
}
