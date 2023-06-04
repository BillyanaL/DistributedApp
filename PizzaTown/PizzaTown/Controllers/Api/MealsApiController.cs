using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaTown.Data;
using PizzaTown.Data.Models;
using PizzaTown.Services.Models.Meals;

namespace PizzaTown.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MealsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Meal>>> GetMeals()
        {
            var meals = await _context.Meals.ToListAsync();
            return Ok(meals);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Meal>> GetMeal(Guid id)
        {
            var meal = await _context.Meals
                .Include(x => x.Author)
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (meal == null)
            {
                return NotFound();
            }

            return meal;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeal(Guid id, [FromBody] MealFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model state errors.");
            }

            var meal = await _context.Meals.FirstOrDefaultAsync(x => x.Id == id);

            if (meal == null)
            {
                return NotFound();
            }

            meal.Name = model.Name;
            meal.Description = model.Description;
            meal.ImageUrl = model.ImageUrl;
            meal.CategoryId = model.CategoryId;
            meal.Price = model.Price;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Meal>> PostMeal(Meal meal)
        {
            try
            {
                _context.Meals.Add(meal);
                await _context.SaveChangesAsync();
                return Ok(meal.Id);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteMeal(Guid id)
        {
            var meal = await _context.Meals.FindAsync(id);

            if (meal == null)
            {
                return NotFound();
            }

            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}