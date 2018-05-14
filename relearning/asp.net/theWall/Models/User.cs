using System;
using System.ComponentModel.DataAnnotations;

namespace theWall.Models
{
    public abstract class BaseEntity {}

    public class User : BaseEntity
    {
        public int id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}