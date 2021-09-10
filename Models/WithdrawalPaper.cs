using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace test.Models
{
    public class WithdrawalPaper
    {
        [Key]
        public string IdWP { get; set; }
        [Display(Name = "Saving Book ID")]
        public string IdSB { get; set; }
        [ForeignKey("IdSB")]
        public SavingBook SavingBook { get; set; }
        [Display(Name = "Customer ID")]
        public string IdCust { get; set; }
        [ForeignKey("IdCust")]
        public Customer Customer { get; set; }
        [Display(Name = "Staff ID")]
        public string IdS { get; set; }
        [ForeignKey("IdS")]
        public Staff Staff { get; set; }
        [Display(Name = "Bank ID")]
        public string IdBank { get; set; }
        [ForeignKey("IdBank")]
        public Bank Bank { get; set; }
        [Display(Name = "Withdrawal")]
        [RegularExpression(@"((\d+)((\.\d{1,2})?))$", ErrorMessage = "Please enter valid integer or decimal number with 2 decimal places.")]
        public double WithdrawalsWP { get; set; }
        [Display(Name = "Transaction Time")]
        public DateTime TransactionTimeWP { get; set; }
    }
}
