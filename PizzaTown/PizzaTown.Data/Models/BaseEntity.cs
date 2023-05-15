using System.ComponentModel.DataAnnotations;

namespace PizzaTown.Data.Models
{
    using static Common.DataConstants;
    public class BaseEntity
    {
        [Required] 
        public Guid Id { get; set; }

        [Required][MaxLength(NameMaxLength)] 
        public string Name { get; set; } = null!;
    }
}