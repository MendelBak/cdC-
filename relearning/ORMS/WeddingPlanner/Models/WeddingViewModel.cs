using System;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
    // This is a custom validation that performs a check to ensure the submitted date was in the past and not in the future
    public class MyDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)// Return a boolean value: true == IsValid, false != IsValid
        {
            DateTime d = Convert.ToDateTime(value);
            return d >= DateTime.Now; //Dates Less than or equal to today are valid (true)
        }
    }
    public class WeddingsViewModel
    {
        [Required]
        public string Bride { get; set; }

        [Required]
        public string Groom { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [MyDate(ErrorMessage="You cannot submit a review for a date in the past!")]
        public DateTime Date { get; set; }
    }
}