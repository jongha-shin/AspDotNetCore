using System;
using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkCoreStudy.Entities
{
    public class Login_infors
    {
        [Key]
        public int SeqId { get; set; }
        public string StaffName { get; set; }
    }
}
