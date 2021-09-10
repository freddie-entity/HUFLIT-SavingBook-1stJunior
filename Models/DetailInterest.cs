using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace test.Models
{
    public class DetailInterest
    {
        [Key]
        public string IdDI { get; set; }
        public string IdInterest { get; set; }
        [ForeignKey("IdInterest")]
        public Interest Interest { get; set; }
        public string IdTerm { get; set; }
        [ForeignKey("IdTerm")]
        [RegularExpression(@"((\d+)((\.\d{1,2})?))$", ErrorMessage = "Please enter valid integer or decimal number with 2 decimal places.")]
        public Term Term { get; set; }

        public double InterestRateDI { get; set; }
    }
}
