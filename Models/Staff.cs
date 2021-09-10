using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace test.Models
{
    public class Staff
    {
        [Key]
        [Display(Name = "Staff ID")]
        public string IdS { get; set; }
        [Display(Name = "Name")]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Upper and lower letters only!")]
        public string NameS { get; set; }
        [Display(Name = "Bank")]
        public string IdBank { get; set; }
        [ForeignKey("IdBank")]
        public Bank Bank { get; set; }
        [Display(Name = "Birthday")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MMM/dd}")]

        public DateTime DOBS { get; set; }
        [Display(Name = "National ID")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only alphabets and numbers allowed.")]
        public string IDCardS { get; set; }
        [Display(Name = "Granted Day")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MMM/dd}")]
        public DateTime IDCardGrantedDayS { get; set; }
        [Display(Name = "Position")]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Upper and lower letters only!")]
        public string PositionS { get; set; }
        [Display(Name = "Phone")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Input must be numeric")]
        public string PhoneS { get; set; }
        [Display(Name = "Status")]
        public bool WorkingStatus { get; set; }
        [Display(Name = "Start")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MMM/dd}")]
        public DateTime StartWorking { get; set; }
        [Display(Name = "End")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MMM/dd}")]
        public DateTime? EndWorking { get; set; }
        public List<SavingBook> SavingBooks { get; set; }
        public List<DepositPaper> DepositPapers { get; set; }
        public List<WithdrawalPaper> WithdrawalPapers { get; set; }

    }
}
