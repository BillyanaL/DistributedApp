using Microsoft.EntityFrameworkCore;
using PizzaTown.Data;
using PizzaTown.Data.Models;
using System.Net.Http;
using Newtonsoft.Json;
using PizzaTown.Services.Models.Meals;

namespace PizzaTown.Services
{
    public class MealService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public MealService(ApplicationDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<MealListingModel>> GetAll()
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://localhost:7119/api/MealsApi")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            var resultAsString = await httpResponseMessage.Content.ReadAsStringAsync();
            var meals = JsonConvert.DeserializeObject<IEnumerable<MealListingModel>>(resultAsString)!.ToList();

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