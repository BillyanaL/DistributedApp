using System.ComponentModel.DataAnnotations;
using PizzaTown.Data.Models;

namespace PizzaTown.Models.Meals
{
    public class MealFormModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(2000, MinimumLength = 20)]
        public string Description { get; set; } = null!;

        [Required]
        [Url]
        public string ImageUrl { get; set; } = null!;

        public decimal Price { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        public IEnumerable<Category> Categories { get; set; } = new HashSet<Category>();
    }
}