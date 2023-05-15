using System.ComponentModel.DataAnnotations;

namespace PizzaTown.Data.Models
{
    public class UserMeal
    {
        [Required] 
        public string UserId { get; set; } = null!;

        public User User { get; set; } = null!;

        [Required] 
        public Guid MealId { get; set; }
        public Meal Meal { get; set; } = null!;
    }
}