using System;
using System.Collections.Generic;

namespace Hanbiza.Models
{
    public partial class VacationApprove
    {
        public int Seqid { get; set; }
        public string Dname { get; set; }
        public string BizNum { get; set; }
        public int VacId { get; set; }
        public int ApproveId { get; set; }
        public string ApproveName { get; set; }
        public int DeepNum { get; set; }
        public string ProcessPoint { get; set; }
        public string RereaSon { get; set; }
        public string Aresult { get; set; }
        public DateTime? Regdate { get; set; }
    }
}
