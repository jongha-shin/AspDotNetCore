using System;
using System.ComponentModel.DataAnnotations;

namespace ideaAppCore.Models
{
    public class Idea
    {
        public int Id { get; set; }
        [Required]
        public String Note { get; set; }

    }
}
