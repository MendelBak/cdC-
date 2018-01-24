using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DojoLeague.Models
{
    public abstract class BaseEntity { }

    public class Dojo : BaseEntity
    {
        [Key]
        public long dojoID { get; set; }

        [Required]
        [MinLength(4, ErrorMessage = "Dojo Name must be at least 4 characters")]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        public string Location { get; set; }

        [MinLength(5)]
        public string Info {get; set; }

        public ICollection<Ninja> ninja { get; set; }


    }
}
