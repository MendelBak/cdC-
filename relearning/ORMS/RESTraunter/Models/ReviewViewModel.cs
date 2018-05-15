using System;
using System.ComponentModel.DataAnnotations;

namespace RESTraunter.Models
{
    // This is a custom validation that performs a check to ensure the submitted date was in the past and not in the future
    public class MyDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)// Return a boolean value: true == IsValid, false != IsValid
        {
            DateTime d = Convert.ToDateTime(value);
            return d <= DateTime.Now; //Dates Less than or equal to today are valid (true)
        }
    }

    public class ReviewViewModel : BaseEntity
    {
        [Required]
        [Display(Name = "Reviewer Name")]
        public string ReviewerName { get; set; }

        [Required]
        public string RestaurantName { get; set; }

        [Required]
        public string Review { get; set; }

        [Required]
        [Range(1,5)]
        public int Stars { get; set; }

        [Required]
        [MyDate(ErrorMessage="You cannot submit a review for a date in the future!")]
        public DateTime DateOfVisit { get; set; }
    }
}