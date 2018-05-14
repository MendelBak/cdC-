using System;
using System.ComponentModel.DataAnnotations;

namespace theWall.Models
{
    public class RegisterViewModel : BaseEntity
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage="Password must match")]
        public string ConfirmPassword { get; set; }
    }
}