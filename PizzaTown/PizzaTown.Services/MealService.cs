﻿using Microsoft.EntityFrameworkCore;
using PizzaTown.Data;
using PizzaTown.Data.Models;
using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json;
using PizzaTown.Services.Models.Meals;

namespace PizzaTown.Services
{
    public class MealService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;

        public MealService(ApplicationDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient();
        }

        public async Task<IEnumerable<MealListingModel>> GetAll()
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://localhost:7119/api/MealsApi")
            };

            var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);
            var resultAsString = await httpResponseMessage.Content.ReadAsStringAsync();
            var meals = JsonConvert.DeserializeObject<IEnumerable<MealListingModel>>(resultAsString)!.ToList();

            return meals;
        }

        public async Task<Meal?> GetById(Guid id)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://localhost:7119/api/MealsApi/{id}")
            };

            var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);
            var resultAsString = await httpResponseMessage.Content.ReadAsStringAsync();
            var meal = JsonConvert.DeserializeObject<Meal>(resultAsString);

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

            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7119/api/MealsApi"),
                Content = JsonContent.Create(meal)
            };

            var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);
            var resultAsString = await httpResponseMessage.Content.ReadAsStringAsync();
            var id = JsonConvert.DeserializeObject<Guid>(resultAsString);

            return id;
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

        public async Task<bool> Delete()
        {
            //var entity =  await GetById(meal.Id);
            //_context.Meals.Remove(entity);
            //await _context.SaveChangesAsync();
            return true;
        }
    }
}