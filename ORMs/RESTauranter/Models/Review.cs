using System;
using System.ComponentModel.DataAnnotations;

namespace RESTauranter.Models
{

    public abstract class BaseEntity {}
    public class Review : BaseEntity
    {
        [Key]
        [Required]
        public int Review_Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Reviewer_Name { get; set; }

        [Required]
        [MinLength(10)]
        public string Comment { get; set; }

        [Required]
        [MinLength(2)]
        public string Restaurant { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required(ErrorMessage="Please select the date on which you visited the restraurant.")]
        public DateTime Date_Of_Visit { get; set; }
    }
}
