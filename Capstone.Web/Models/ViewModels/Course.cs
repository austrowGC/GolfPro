using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Web.Models.ViewModels
{
    public class Course
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public int NumberOfHoles { get; set; }

        [Required(ErrorMessage = "This field is required and must be greater than 900 and less than 9999")]
        public int LengthInYards { get; set; }

        [Required(ErrorMessage = "This field is required and must be greater than 10 and less than 99")]
        public int Par { get; set; }
    }
}