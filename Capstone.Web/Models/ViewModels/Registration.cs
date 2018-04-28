using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.ViewModels
{
    public class Registration
    {
        [Required(ErrorMessage="This field is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,128}", ErrorMessage = "Password required to be longer than 8 characters and contain lower case, upper case, number, and special character.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "This field must match the password entered above")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}