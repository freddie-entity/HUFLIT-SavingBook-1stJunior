using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace test.Models
{
    public class Report
    {
        [Key]
        [Display(Name = "Report ID")]
        public string IdReport { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MMM/dd}")]
        public DateTime From { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MMM/dd}")]
        public DateTime To { get; set; }
        public List<DetailReport> DetailReports { get; set; }
    }
}
