using System.ComponentModel.DataAnnotations;

namespace PizzaTown.Data.Models
{
    using static Common.DataConstants;
    public class Meal : BaseEntity
    {
        [Required] 
        public string ImageUrl { get; set; } = null!;

        public decimal Price { get; set; }

        [Required] 
        [MaxLength(DescriptionMaxLength)] 
        public string Description { get; set; } = null!;

        [Required] 
        public Guid CategoryId { get; set; } 

        public Category Category { get; set; } = default!;
    }
}