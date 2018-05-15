using System;

namespace RESTraunter.Models
{
    public abstract class BaseEntity{}

    public class Reviews : BaseEntity
    {
        public int Id { get; set; }
        public string ReviewerName { get; set; }
        public string RestaurantName { get; set; }
        public string Review { get; set; }
        public int Stars { get; set; }
        public int Helpful { get; set; }
        public DateTime DateOfVisit { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}