using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PizzaTown.Models;
using PizzaTown.Services;

namespace PizzaTown.Controllers
{
    public class MealsController : Controller
    {
        private readonly MealService _mealService;
        private readonly CategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly AutoMapper.IConfigurationProvider _configuration;

        public MealsController(IMapper mapper, MealService mealService, CategoryService categoryService)
        {
            _mapper = mapper;
            _configuration = mapper.ConfigurationProvider;
            _mealService = mealService;
            _categoryService = categoryService;
        }

        public async Task<ActionResult> Index()
        {
            var meals = await _mealService.GetAll();
            return View(meals);
        }

        public async Task<ActionResult> Details(Guid id)
        {
            var meal = await _mealService.GetById(id);

            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }

        public async Task<ActionResult> Create()
        {
            var categories = await _categoryService.GetAll();
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

            var mealId = await _mealService.Create(model.Name, model.Description, model.ImageUrl, model.CategoryId,
                model.Price);

            return RedirectToAction(nameof(Details), new { id = mealId });
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            var meal = await _mealService.GetById(id);

            if (meal == null)
            {
                return NotFound();
            }

            var categories = await _categoryService.GetAll();

            // mapper
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, [Bind("Name, ImageUrl, Description, CategoryId, Price")] MealFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Create), model);
            }

            var meal = await _mealService.GetById(id);

            if (meal == null)
            {
                return NotFound();
            }

            await _mealService.Edit(meal, model.Name, model.Description, model.ImageUrl, model.CategoryId, model.Price);
            
            return RedirectToAction(nameof(Details), new { id = meal.Id });
        }
        
        public async Task<ActionResult> Delete(Guid id)
        {
            var meal = await _mealService.GetById(id);

            if (meal == null)
            {
                return NotFound();
            }

            await _mealService.Delete(meal);
            return RedirectToAction(nameof(Index));
        }
    }
}