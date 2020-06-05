using System;
using System.Collections.Generic;

namespace Hanbiza.Models
{
    public partial class VacationList
    {
        public int Seqid { get; set; }
        public string Dname { get; set; }
        public string BizNum { get; set; }
        public int StaffId { get; set; }
        public string Vicname { get; set; }
        public double? UseTime { get; set; }
        public DateTime Snal { get; set; }
        public DateTime Enal { get; set; }
        public int? ProCdeep { get; set; }
        public string AllProCess { get; set; }
        public string VicReaSon { get; set; }
        public string HbzMsend { get; set; }
        public DateTime? HbzMyymmdd { get; set; }
        public DateTime? Regdate { get; set; }
    }
}
