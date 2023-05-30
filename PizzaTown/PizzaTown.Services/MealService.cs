using Microsoft.EntityFrameworkCore;
using PizzaTown.Data;
using PizzaTown.Data.Models;

namespace PizzaTown.Services
{
    public class MealService
    {
        private readonly ApplicationDbContext _context;

        public MealService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Meal>> GetAll(string? category)
        {
            List<Meal> meals;

            if (category == null)
            {
                meals = await _context.Meals.ToListAsync();
            }
            else
            {
                meals = await _context.Meals
                    .Include(m => m.Category)
                    .Where(m => m.Category.Name.ToLower() == category.ToLower())
                    .ToListAsync();
            }

            return meals;
        }

        public async Task<Meal?> GetById(Guid id)
        {
            var meal = await _context.Meals.Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);

            return meal;
        }

        public async Task<Guid> Create(string name, string description, string imageUrl, Guid categoryId, decimal price, string authorId)
        {
            var meal = new Meal
            {
                Id = new Guid(),
                Name = name,
                Description = description,
                CategoryId = categoryId,
                ImageUrl = imageUrl,
                Price = price,
                AuthorId = authorId
            };

            await _context.Meals.AddAsync(meal);
            await _context.SaveChangesAsync();

            return meal.Id;
        }

        public async Task<Guid> Edit(Meal meal, string name, string description, string imageUrl, Guid categoryId,
            decimal price)
        {
            meal.Name = name;
            meal.Description = description;
            meal.ImageUrl = imageUrl;
            meal.CategoryId = categoryId;
            meal.Price = price;

            await _context.SaveChangesAsync();
            return meal.Id;
        }

        public async Task<bool> Delete(Meal meal)
        {
            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}