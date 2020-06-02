using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityFrameworkCoreStudy.Entities
{
    public class 출퇴근기록_집계표s
    {
        [Key]
        public int StaffID { get; set; }
        public DateTime 시작일 { get; set; }
        public DateTime 종료일 { get; set; }
        public int 기준일 { get; set; }

    }
}
