
using System;

namespace HanbizaMVC.Models
{
    public partial class 인사평가
    {
        public int SEQID { get; set; }
        public string Dname { get; set; }
        public string BizNum { get; set; }
        public int ID { get; set; }
        public string 평가지 { get; set; }
        public int 피평가자ID { get; set; }
        public string 피평가자 { get; set; }
        public string 구분 { get; set; }
        public string 세부사항 { get; set; }
        public double 배점 { get; set; }
        public string 평가기준 { get; set; }
        public string 등급 { get; set; }
        public double 점수 { get; set; }
        public string 등록인 { get; set; }
        public string 평가의견 { get; set; }
        public int 평가자ID { get; set; }
        public string 마감 { get; set; }
        public int 답변ID { get; set; }
        public string 한비자전송 { get; set; }
        public DateTime Regdate { get; set; }

        public int DetailSEQID { get; set; }
        public string 등급목록 { get; set; }
        public double 배점목록 { get; set; }
        public long colspan { get; set; }


    }
}
