using System;

namespace HanbizaMVC.Models
{
    public partial class 시설물
    {
        public int SeqID { get; set; }
        public string Dname { get; set; }
        public string BizNum { get; set; }
        public string Gubun1 { get; set; }
        public string Gubun2 { get; set; }
        public string 등록인 { get; set; }
        public string 삭제 { get; set; }
        public DateTime? Regdate { get; set; }
        
    }
}
