using System;
using System.ComponentModel.DataAnnotations;

namespace FormSubmission.Models
{
    public abstract class BaseEntity {}
    
    public class User : BaseEntity
    {
        [Required]
        [MinLength(4, ErrorMessage="First Name must be at least 4 characters")]
        public string FirstName { get; set; }
        [Required]
        [MinLength(4, ErrorMessage="Last Name must be at least 4 characters")]
        public string LastName { get; set; }
        [Required]
        [Range(1, 120)]
        public int Age { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password) ]
        public string Password { get; set; }
    }
}