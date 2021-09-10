using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace test.Models
{
    public class BookType
    {
        [Key]
        [Display(Name = "Book Type ID")]
        public string IdBookType { get; set; }
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Upper and lower letters only!")]
        [Display(Name = "Book Type")]
        public string NameBookType { get; set; }
        public List<DetailReport> DetailReports { get; set; }
        public List<SavingBook> SavingBooks { get; set; }
        public List<WithdrawalPaper> WithdrawalPapers { get; set; }
    }
}
