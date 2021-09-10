using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace test.Models
{
    public class Interest
    {
        [Key]
        [Display(Name = "Interest ID")]
        public string IdInterest { get; set; }
        [Display(Name = "Applied From")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MMM/dd}")]
        public DateTime? AppliedFrom { get; set; }
        [Display(Name = "Applied To")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MMM/dd}")]
        public DateTime? AppliedTo { get; set; }
        public List<DetailInterest> DetailInterests { get; set; }
    }
}
