using Microsoft.EntityFrameworkCore;
using PizzaTown.Data;
using PizzaTown.Data.Models;

namespace PizzaTown.Services
{
    public class MealService
    {
        private ApplicationDbContext _context;

        public MealService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Meal>> GetAll()
        {
            var meals = await _context.Meals.ToListAsync();
            return meals;
        }

        public async Task<Meal?> GetById(Guid id)
        {
            var meal = await _context.Meals.Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);

            return meal;
        }

        public async Task<Guid> Create(string name, string description, string imageUrl, Guid categoryId, decimal price)
        {
            var meal = new Meal
            {
                Id = new Guid(),
                Name = name,
                Description = description,
                CategoryId = categoryId,
                ImageUrl = imageUrl,
                Price = price
            };

            await _context.Meals.AddAsync(meal);
            await _context.SaveChangesAsync();

            return meal.Id;
        }
    }
}
