using PizzaTown.Data.Models;
using System.Net.Http.Json;
using Newtonsoft.Json;
using PizzaTown.Services.Models.Meals;

namespace PizzaTown.Services
{
    public class MealService
    {
        private const string ApiBaseUrl = "https://localhost:7119/api/MealsApi";
        private readonly HttpClient _httpClient;

        public MealService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IEnumerable<MealListingModel>> GetAll()
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(ApiBaseUrl)
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
                RequestUri = new Uri($"{ApiBaseUrl}/{id}")
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
                RequestUri = new Uri($"{ApiBaseUrl}"),
                Content = JsonContent.Create(meal)
            };

            var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);
            var resultAsString = await httpResponseMessage.Content.ReadAsStringAsync();
            var id = JsonConvert.DeserializeObject<Guid>(resultAsString);

            return id;
        }

        public async Task<bool> Edit(Guid id, MealFormModel model)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{ApiBaseUrl}/{id}"),
                Content = JsonContent.Create(model)
            };

            var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);
            return httpResponseMessage.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(Guid id)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{ApiBaseUrl}/{id}"),
                Content = JsonContent.Create(id)
            };

            var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);
            return httpResponseMessage.IsSuccessStatusCode;
        }
    }
}