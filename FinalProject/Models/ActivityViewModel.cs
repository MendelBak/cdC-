using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public class ActivityViewModel : BaseEntity
    {

        [Required]
        public int AdminId { get; set; }

        
        [Required]
        [MinLength(2)]
        public string Title { get; set; }


        [Required]
        public DateTime Time { get; set; }


        [Required]
        public DateTime Date { get; set; }


        [Required]
        [Range(1, 10000, ErrorMessage="Sorry, Please only enter a number from 1 to 10,000")]
        public int Duration { get; set; }


        [Required]
        public string DurationType { get; set; }


        [Required]
        public string Description { get; set; }



    }

}