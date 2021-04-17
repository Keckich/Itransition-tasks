using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace test5.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime RegisterDate { get; set; }
        public DateTime LoginDate { get; set; }
        public string Status { get; set; } = "Default";
    }
}
