using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace test.ViewModels
{
    public class RegisterViewModels
    {
        [Required(ErrorMessage = "Please type username!"), StringLength(50)]
        [Remote(action: "IsUsernameInUse", controller: "Account")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Display(Name ="Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password does not match!!!")]
        public string ConfirmPassword { get; set; }
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only letters and numbers allowed.")]
        public string IdS { get; set; }

    }
}
