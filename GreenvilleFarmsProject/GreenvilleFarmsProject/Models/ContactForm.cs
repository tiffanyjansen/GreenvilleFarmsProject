using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GreenvilleFarmsProject.Models
{
    public class ContactForm
    {
        [RegularExpression(@"(\s|[a-xA-Z])+", ErrorMessage = "Please enter your name")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Please enter your email address")]
        [RegularExpression(@"(\w|\.)+\@(\w|\.)+\.(\w|\.)+", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a subject")]
        public string Subject { get; set; }

        public string EmailContents { get; set; }
    }
}