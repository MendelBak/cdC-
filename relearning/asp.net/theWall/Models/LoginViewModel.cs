using System;
using System.ComponentModel.DataAnnotations;

namespace theWall.Models
{
    public class LoginViewModel : BaseEntity
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}