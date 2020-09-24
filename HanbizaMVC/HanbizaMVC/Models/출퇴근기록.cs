using System;
using System.Collections.Generic;

namespace HanbizaMVC.Models
{
    public partial class 출퇴근기록
    {
        public int Seqid { get; set; }
        public string Dname { get; set; }
        public string BizNum { get; set; }
        public int StaffId { get; set; }
        public DateTime 날짜 { get; set; }
        public int? 주간 { get; set; }
        public string 출근 { get; set; }
        public string 퇴근 { get; set; }
        public double? 지각 { get; set; }
        public double? 외출 { get; set; }
        public double? 총근로 { get; set; }
        public double? 소정근로 { get; set; }
        public double? 초과근로 { get; set; }
        public double? 주휴 { get; set; }
        public double? 근태조정 { get; set; }
        public double? 연장근로 { get; set; }
        public double? 연장조정 { get; set; }
        public double? 야간근로 { get; set; }
        public double? 휴일근로 { get; set; }
        public double? 휴일연장 { get; set; }
        public double? 휴일야간 { get; set; }
        public string 일체크 { get; set; }
        public string 비고 { get; set; }
        public string 등록인 { get; set; }
        public DateTime? Regdate { get; set; }

        public string 요일 { get; set; }
        public string 년 { get; set; }
        public string 월 { get; set; }
    }
}
