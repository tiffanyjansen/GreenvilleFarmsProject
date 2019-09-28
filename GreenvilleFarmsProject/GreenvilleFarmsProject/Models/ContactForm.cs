using System.ComponentModel.DataAnnotations;

namespace GreenvilleFarmsProject.Models
{
    public class ContactForm
    {
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [RegularExpression(@"(\w|\.)+\@(\w|\.)+\.(\w|\.)+", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a subject")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Please enter your message")]
        public string EmailContents { get; set; }
    }
}