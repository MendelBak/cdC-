using System;
using System.ComponentModel.DataAnnotations;

namespace DojoLeague.Models
{
    public class Ninja : BaseEntity
    {
        [Key]
        public long NinjaID { get; set;}

        [Required]
        [MinLength(4, ErrorMessage = "Ninja's name must be at least 4 characters")]
        public string Name { get; set; }

        [Required]
        public int Level { get; set; }
        
        [MinLength(4)]
        public string Description { get; set; }

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }

        public Dojo dojo { get; set; }

    }
}
