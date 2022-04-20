using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Last.Models
{
    public class job
    {
        public int Id { get; set; }
        [DisplayName("اسم الوظيفة")]
        public string jobName { get; set; }
        [DisplayName("وصف الوظيفة الوظيفة")]
        public string jobContent { get; set; }
        [DisplayName("صورة الوظيفة")]
        public string jobImage { get; set; }
        [DisplayName("نوع الوظيفة")]
        public int CategoryId { get; set; }

        public string UserID { get; set; }
        public virtual Category  category { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}