using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace test.Models
{
    public class SavingBook
    {
        [Key]
        [Display(Name = "Saving Book ID")]
        public string IdSB { get; set; }
        [Display(Name = "Customer ID")]
        public string IdCust { get; set; }
        [ForeignKey("IdCust")]
        public Customer Customer { get; set; }
        [Display(Name = "Staff ID")]
        public string IdS { get; set; }
        [ForeignKey("IdS")]
        public Staff Staff { get; set; }
        [Display(Name = "Book Type ID")]
        public string IdBookType { get; set; }
        [ForeignKey("IdBookType")]
        public BookType BookType { get; set; }
        [Display(Name = "Term")]
        public string IdTerm { get; set; }
        [ForeignKey("IdTerm")]       
        public Term Term { get; set; }
        [Display(Name = "Deposit")]
        [RegularExpression(@"((\d+)((\.\d{1,2})?))$", ErrorMessage = "Please enter valid integer or decimal number with 2 decimal places.")]
        public double DepositsSB { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MMM/dd}")]
        [Display(Name = "Open Day")]
        public DateTime OpenDaySB { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MMM/dd}")]
        [Display(Name = "Due Day")]
        public DateTime? DueDaySB { get; set; }
        [Display(Name = "Pay Method")]
        public bool InterestPaymentMethodSB { get; set; }
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Input must be numeric")]
        [Display(Name = "Receiving Account")]
        public string InterestReceivingAccount { get; set; }
        [Display(Name = "Balance")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [RegularExpression(@"((\d+)((\.\d{1,2})?))$", ErrorMessage = "Please enter valid integer or decimal number with 2 decimal places.")]
        public double CurrentBalance { get; set; }
        public List<DepositPaper> DepositPapers { get; set; }
        public List<WithdrawalPaper> WithdrawalPapers { get; set; }
    }
}
