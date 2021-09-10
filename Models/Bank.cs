using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace test.Models
{
    public class Bank
    {
        [Key]
        public string IdBank { get; set; }
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Upper and lower letters only!")]
        [MaxLength(150)]
        [DisplayName("Bank's Name")]
        public string NameBank { get; set; }        
        [MaxLength(200)]
        [DisplayName("Bank's Address")]
        public string AddressBank { get; set; }

        public List<Staff> Staffs { get; set; }
        public List<DepositPaper> DepositPapers { get; set; }
        public List<WithdrawalPaper> WithdrawalPapers { get; set; }

    }
}
