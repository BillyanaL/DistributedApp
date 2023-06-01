using System.ComponentModel.DataAnnotations;
using PizzaTown.Data.Models;
using static PizzaTown.Data.Common.DataConstants;

namespace PizzaTown.Models.Meals
{
    public class MealFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [Url]
        public string ImageUrl { get; set; } = null!;

        [Range(MinPrice, MaxPrice)]
        public decimal Price { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        public IEnumerable<Category> Categories { get; set; } = new HashSet<Category>();
    }
}