using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogLab.Models.Account
{
    public class ApplicationUserLogin
    {
        [Required(ErrorMessage ="Username is required")]
        [MinLength(5, ErrorMessage ="Must be atleast 5-20 characters")]
        [MaxLength(20, ErrorMessage = "Must be atleast 20 characters")]
        public string username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Must be atleast 8 characters")]
        [MaxLength(50, ErrorMessage = "Must be atleast 50 characters")]
        public string password { get; set; }
    }
}
