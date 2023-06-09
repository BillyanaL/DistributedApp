﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaTown.Infrastructure;
using PizzaTown.Models;
using PizzaTown.Services;
using PizzaTown.Services.Models.Meals;
using static PizzaTown.Data.Common.DataConstants;

namespace PizzaTown.Controllers
{
    [Authorize]
    public class MealsController : Controller
    {
        private readonly MealService _mealService;
        private readonly CategoryService _categoryService;
        private readonly IMapper _mapper;

        public MealsController(IMapper mapper, MealService mealService, CategoryService categoryService)
        {
            _mapper = mapper;
            _mealService = mealService;
            _categoryService = categoryService;
        }

        public async Task<ActionResult> Index(int pageIndex = 1)
        {
            var meals = await _mealService.GetAll();
            var paginatedMeals = PaginatedList<MealListingModel>.Create(meals, pageIndex, MealsPerPage);
            return View(paginatedMeals);
        }

        public async Task<ActionResult> Details(Guid id)
        {
            var meal = await _mealService.GetById(id);

            if (meal == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var mealDetailedModel = _mapper.Map<MealDetailedModel>(meal);
            return View(mealDetailedModel);
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

            var userId = this.User.GetId();
            var mealId = await _mealService.Create(model.Name, model.Description, model.ImageUrl, model.CategoryId,
                model.Price, userId!);

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
            var mealFormModel = _mapper.Map<MealFormModel>(meal);
            mealFormModel.Categories = categories;

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

            await _mealService.Edit(id,  model);

            return RedirectToAction(nameof(Details), new { id = meal.Id });
        }

        public async Task<ActionResult> Delete(Guid id)
        {
            var meal = await _mealService.GetById(id);

            if (meal == null)
            {
                return NotFound();
            }

            await _mealService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}