using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaTown.Data;
using PizzaTown.Data.Models;
using PizzaTown.Models;

namespace PizzaTown.Controllers
{
    public class MealsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly AutoMapper.IConfigurationProvider _configuration;

        public MealsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _configuration = mapper.ConfigurationProvider;
        }
        
        public async Task<ActionResult> Index()
        {
            var meals = await _context.Meals.ToListAsync();
            return View(meals);
        }
        
        public async Task<ActionResult> Details(Guid id)
        {
            var meal = await _context.Meals.Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }
        
        public async Task<ActionResult> Create()
        {
            var categories = await _context.Categories.ToListAsync();
            var mealModel = new MealFormModel
            {
                Categories = categories
            };

            return View(mealModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Name, ImageUrl, Description, CategoryId, Price")] MealFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var meal = new Meal
            {
                Id = new Guid(),
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                ImageUrl = model.ImageUrl,
                Price = model.Price
            };

            await _context.Meals.AddAsync(meal);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new {id = meal.Id});
        }
        
        public async Task<ActionResult> Edit(Guid id)
        {
            var meal = await _context.Meals.FirstOrDefaultAsync(x => x.Id == id);

            if (meal == null)
            {
                return NotFound();
            }

            var categories = await _context.Categories.ToListAsync();

            var mealFormModel = new MealFormModel
            {
                Name = meal.Name,
                Description = meal.Description,
                ImageUrl = meal.ImageUrl,
                CategoryId = meal.CategoryId,
                Price = meal.Price,
                Categories = categories
            };

            return View(nameof(Create), mealFormModel);
        }

        // POST: MealsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MealsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MealsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
