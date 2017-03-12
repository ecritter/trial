using System.ComponentModel.DataAnnotations;

namespace Xm.Trial.Models
{
    public class ContactUsForm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }
    }
}