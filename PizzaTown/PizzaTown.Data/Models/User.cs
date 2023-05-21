using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PizzaTown.Data.Models
{
    public class User : IdentityUser
    {
        [Required] 
        public string RoleId { get; set; } = null!;

        public Role Role { get; set; } = null!;

        public IEnumerable<UserMeal> Cart { get; set; } = new HashSet<UserMeal>();
    }
}