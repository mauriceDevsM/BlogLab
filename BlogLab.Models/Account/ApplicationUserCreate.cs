using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Xunit;

namespace BlogLab.Models.Account
{
    public class ApplicationUserCreate : ApplicationUserLogin
    {
        [MinLength(10, ErrorMessage = "Must be atleast 10-50 characters")]
        [MaxLength(50, ErrorMessage = "Must be atleast 50 characters")]
        public string Fullname { get; set; }
        
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email format")]
        [MaxLength(50, ErrorMessage = "Must be atmost 50 characters")]
        public string Email { get; set; }
    }
}
