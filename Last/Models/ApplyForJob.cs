using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Last.Models
{
    public class ApplyForJob
    {
        public int Id { get; set; }
       [Display(Name ="الرسالة")]
        public string Message { get; set; }
        [Display(Name = "التاريخ والوقت")]
        public DateTime ApplyDate { get; set; }
        public int JobId { get; set; }
        public string UserId  { get; set; }

        public virtual job job { get; set; }
        public virtual ApplicationUser user { get; set; }
    }
}