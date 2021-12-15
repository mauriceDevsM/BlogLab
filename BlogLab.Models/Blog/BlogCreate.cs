using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogLab.Models.Blog
{
    public class BlogCreate
    {
        public int BlogId { get; set; }

        [Required(ErrorMessage = "Ttitle is required")]
        [MinLength(10, ErrorMessage = "Must be atleast 10-50 characters")]
        [MaxLength(50, ErrorMessage = "Must be atmost 50 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required")]
        [MinLength(300, ErrorMessage = "Must be atleast 300 characters")]
        [MaxLength(3000, ErrorMessage = "Must be atmost 3000 characters")]
        public string Content { get; set; }
        public int? Photo { get; set; }
    }
}
