using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PizzaTown.Data.Models
{
    using static Common.DataConstants;
    public class Meal : BaseEntity
    {
        [Required] 
        public string ImageUrl { get; set; } = null!;

        [Range(MinPrice, MaxPrice)]
        public decimal Price { get; set; }

        [Required] 
        [MaxLength(DescriptionMaxLength)] 
        public string Description { get; set; } = null!;

        [Required] 
        public Guid CategoryId { get; set; } 

        public Category Category { get; set; } = null!;


        [Required]
        public string AuthorId { get; set; } = null!;

        public IdentityUser Author { get; set; } = null!;
    }
}