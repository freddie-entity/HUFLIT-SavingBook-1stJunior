using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace test.Models
{
    public class Customer
    {
        [Key]
        [Display(Name = "Customer Code")]
        public string IdCust { get; set; }
        [DisplayName("Name")]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Upper and lower letters only!")]
        public string NameCust { get; set; }
        [Display(Name = "Birthday")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime DOBCust { get; set; }
        [DisplayName("Address")]
        
        public string AddressCust { get; set; }
        [DisplayName("Phone")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Input must be numeric")]
        public string PhoneCust { get; set; }
        [DisplayName("National ID")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only alphabets and numbers allowed.")]
        public string IDCardCust { get; set; }
        [Display(Name = "Granted Day")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime IDCardGrantedDayCust { get; set; }
        public List<SavingBook> SavingBooks { get; set; }
        public List<DepositPaper> DepositPapers { get; set; }
        public List<WithdrawalPaper> WithdrawalPapers { get; set; }
    }
}
