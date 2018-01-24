using System;
using System.ComponentModel.DataAnnotations;

namespace LostInTheWoods.Models
{
    public abstract class BaseEntity { }

    public class Trail : BaseEntity
    {
        [Key]
        public long id { get; set; }


        [Required]
        [MinLength(4, ErrorMessage = "Trail Name must be at least 4 characters")]
        public string Name { get; set; }


        [Required]
        [MinLength(4, ErrorMessage = "Description must be at least 4 characters")]
        public string Description { get; set; }


        [Required]
        public int Length { get; set; }


        [Required]
        public int ElevationGain { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Latitude { get; set; }
    }
}
