using System;

namespace HanbizaMVC.ViewModel
{
    public class TotalAttendence
    {
        public decimal 총근로 { get; set; }
        public decimal 소정근로 { get; set; }
        public decimal 근태조정 { get; set; }
        public decimal 초과근로 { get; set; }
        public decimal 연장근로 { get; set; }
        public decimal 야간근로 { get; set; }
        public decimal 휴일근로 { get; set; }
        public decimal 휴일연장 { get; set; }
        public decimal 휴일야간 { get; set; }
    }
}
