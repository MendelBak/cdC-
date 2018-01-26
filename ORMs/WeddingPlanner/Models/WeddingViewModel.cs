using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
    public class WeddingViewModel : BaseEntity
    {
        [Required]
        [MinLength(2, ErrorMessage="Husband's Name must be at least 2 characters.")]
        public string HusbandName { get; set; }


        [Required]
        [MinLength(2, ErrorMessage="Wife's Name must be at least 2 characters.")]
        public string WifeName { get; set; }


        [Required]
        public String Address { get; set; }

        [Required]
        public DateTime DateOfWedding { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}