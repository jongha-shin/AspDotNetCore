using System;
using System.Collections.Generic;

namespace Hanbiza.Models
{
    public partial class 연차대장
    {
        public int Seqid { get; set; }
        public string Dname { get; set; }
        public string BizNum { get; set; }
        public int StaffId { get; set; }
        public DateTime 입사일 { get; set; }
        public DateTime? 연차발생일 { get; set; }
        public double? 근속년수 { get; set; }
        public double? 발생연차 { get; set; }
        public double? 잔여일수 { get; set; }
        public double? 이월조정추가 { get; set; }
        public string 사용기간 { get; set; }
        public string 연차구분 { get; set; }
        public string 등록인 { get; set; }
        public DateTime? Regdate { get; set; }
    }
}
