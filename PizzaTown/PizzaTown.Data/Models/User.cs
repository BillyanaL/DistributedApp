using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PizzaTown.Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        public Guid RoleId { get; set; }

        public Role Role { get; set; } = default!;

        public IEnumerable<Meal> Cart { get; set; } = new HashSet<Meal>();
    }
}
