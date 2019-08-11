using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GreenvilleFarmsProject.Models
{
    public class ContactForm
    {
        [Required, Display(Name="Your name")]
        public string Name { get; set; }
        
        [Required, Display(Name="Your email address")]
        [RegularExpression(@"(\w|\.)+\@(\w|\.)+\.(\w|\.)+", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required, Display(Name="Subject")]
        public string Subject { get; set; }
        [Required, Display(Name="Message...")]
        public string EmailContents { get; set; }
    }
}