using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace test.Models
{
    public class AppUser : IdentityUser
    {
        [Display(Name = "Staff Code")]
        public string IdS { get; set; }
        [ForeignKey("IdS")]
        public Staff Staff { get; set; }
    }
}
