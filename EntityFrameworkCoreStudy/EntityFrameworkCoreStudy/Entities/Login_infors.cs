using System;
using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkCoreStudy.Entities
{
    public class Login_infors
    {
        [Key]
        public int SeqId { get; set; }
        public string StaffName { get; set; }
        public string CompanyName { get; set; }

        public int StaffID { get; set; }
        public 출퇴근기록_집계표s 출퇴근기록_집계표s { get; set; }
    }
}
