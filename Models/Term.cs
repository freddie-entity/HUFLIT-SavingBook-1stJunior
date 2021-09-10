using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace test.Models
{
    public class Term
    {
        [Key]
        [Display(Name = "Term ID")]
        public string IdTerm { get; set; }
        [Display(Name = "Term")]
        [RegularExpression(@"((\d+)((\.\d{1,2})?))$", ErrorMessage = "Please enter valid integer or decimal number with 2 decimal places.")]
        public string NameTerm { get; set; }
        [Display(Name = "Rate")]
        [RegularExpression(@"((\d+)((\.\d{1,2})?))$", ErrorMessage = "Please enter valid integer or decimal number with 2 decimal places.")]
        public double? InterestRateT { get; set; }
        public List<DetailInterest> DetailInterests { get; set; }
        public List<SavingBook> SavingBooks { get; set; }
    }
}
