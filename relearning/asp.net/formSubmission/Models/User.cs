using System;
using System.ComponentModel.DataAnnotations;

namespace formSubmission.Models
{
    public class User : BaseEntity
    {
        [Required]
        [MinLength(4, ErrorMessage="First Name must be at least 4 characters long.")]
        public string firstName { get; set; }

        [Required]
        [MinLength(4)]
        public string lastName { get; set; }

        [Required]
        [RegularExpression("^[0-9]+$")]
        public int age { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
    
}