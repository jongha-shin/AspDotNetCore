using System;

namespace HanbizaMVC.Models
{
    public partial class 회사별메뉴
    {
        public int Seqid { get; set; }
        public string BizNum { get; set; }
        public string CompanyName { get; set; }
        public Boolean 근태보기 {get; set;}
        public Boolean OT신청 {get; set;}
        public Boolean 휴가신청 {get; set;} 
        public Boolean 휴가결재 {get; set;} 
        public Boolean 연차보기 {get; set;} 
        public Boolean 급여명세서 {get; set;} 
        public Boolean 확인서명 {get; set;} 
        public Boolean 내문서 {get; set;}
        public Boolean 비밀번호변경 { get; set; }
        public Boolean 로그아웃 { get; set; }
    }
}
