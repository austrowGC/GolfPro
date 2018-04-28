using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.ViewModels
{
    public class Login
    {
        [Required(ErrorMessage = "Required field")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Required field")]
        public string Password { get; set; }

    }
}