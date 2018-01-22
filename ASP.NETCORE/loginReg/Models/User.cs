using System;
using System.ComponentModel.DataAnnotations;

namespace loginReg.Models
{
    public abstract class BaseEntity { }

    public class User : BaseEntity
    {
        [Required]
        [MinLength(4, ErrorMessage = "First Name must be at least 4 characters")]
        public string FirstName { get; set; }


        [Required]
        [MinLength(4, ErrorMessage = "Last Name must be at least 4 characters")]
        public string LastName { get; set; }


        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required]
        [Compare("Password", ErrorMessage = "Password and Confirm Password must match!")]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }


    public class loginUser : BaseEntity
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
    public class AllUsers 
    {
        public User Reg { get; set; }
        public loginUser Log { get; set; }
    }

}
