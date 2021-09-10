using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace test.Models
{
    public class DetailReport
    {
        [Key]
        public string IdDR { get; set; }
        [Display(Name = "Report ID")]
        public string IdReport { get; set; }
        [ForeignKey("IdReport")]
        public Report Report { get; set; }
        [Display(Name = "Book Type ID")]
        public string IdBookType { get; set; }
        [ForeignKey("IdBookType")]
        public BookType BookType { get; set; }
        [Display(Name = "Opened Books")]
        [RegularExpression("[^0-9]", ErrorMessage = "Input must be numeric")]
        public int OpenedBooks { get; set; }
        [Display(Name = "Closed Books")]
        [RegularExpression("[^0-9]", ErrorMessage = "Input must be numeric")]
        public int ClosedBooks { get; set; }
        [Display(Name = "Total Books")]
        [RegularExpression("[^0-9]", ErrorMessage = "Input must be numeric")]
        public int TotalBooks { get; set; }
        [Display(Name = "Total Revenue")]
        [RegularExpression(@"((\d+)((\.\d{1,2})?))$", ErrorMessage = "Please enter valid integer or decimal number with 2 decimal places.")]
        public double TotalRevenue { get; set; }
        [Display(Name = "Total Expense")]
        [RegularExpression(@"((\d+)((\.\d{1,2})?))$", ErrorMessage = "Please enter valid integer or decimal number with 2 decimal places.")]
        public double TotalExpense { get; set; }

    }
}
