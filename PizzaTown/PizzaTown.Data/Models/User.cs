using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PizzaTown.Data.Models
{
    public class User : IdentityUser
    {
        public IEnumerable<Meal> Cart { get; set; } = new HashSet<Meal>();
    }
}
