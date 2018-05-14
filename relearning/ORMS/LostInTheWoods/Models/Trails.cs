using System;
using System.ComponentModel.DataAnnotations;

namespace LostInTheWoods.Models
{
    public abstract class BaseEntity{}

    public class Trails : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set;}
        
        
        [Required]
        public string Description { get; set;}
        
        [Required]
        public int Length{ get; set;}
        
        [Required]
        public int ElevChange { get; set;}
        
        [Required]
        public double Latitude { get; set;}
        
        [Required]
        public double Longitude { get; set;}

    }
}