using System;
using System.Collections.Generic;

namespace Hanbiza.Models
{
    public partial class 공지사항
    {
        public int Seqid { get; set; }
        public string Dname { get; set; }
        public string BizNum { get; set; }
        public string 제목 { get; set; }
        public string 내용 { get; set; }
        public string 등록인 { get; set; }
        public int? VacId { get; set; }
        public DateTime? Regdate { get; set; }
        public int? LoginId { get; set; }
    }
}
