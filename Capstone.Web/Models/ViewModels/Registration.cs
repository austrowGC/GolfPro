using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Registration
    {
        [Required(ErrorMessage="*")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "*")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^((?=.[a-z])(?=.[A-Z])(?=.\d)(?=.[!@#$%&()]))(?=.[\da-zA-Z!@#$%^&*()]).{8,128}$", ErrorMessage = "Password must meet the requirements")]
        public string Password { get; set; }

        [Required(ErrorMessage = "*")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}