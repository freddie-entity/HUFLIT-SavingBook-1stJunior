using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace test.Areas.Admin.ViewModels
{
    public class CreateRoleViewModels
    {
        [Required(ErrorMessage = "Please enter role name!"), StringLength(50)]
        public string RoleName { get; set; }
    }
}
