using System;
using System.Collections.Generic;

namespace HanbizaMVC.Models
{
    public partial class 문서함
    {
        public int SeqId { get; set; }
        public string Dname { get; set; }
        public string BizNum { get; set; }
        public int StaffId { get; set; }
        public string Gubun { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public byte[] FileBlob { get; set; }
        public string 등록인 { get; set; }
        public DateTime Regdate { get; set; }
        public string Signature { get; set; }
        public string SignDown { get; set; }
        public string EditYn { get; set; }
        public string Ftype { get; set; }
        public int? Fsize { get; set; }
    }
}
