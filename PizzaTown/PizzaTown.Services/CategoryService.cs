using Newtonsoft.Json;
using PizzaTown.Data.Models;

namespace PizzaTown.Services
{
    public class CategoryService
    {
        private const string ApiBaseUrl = "https://localhost:7119/api/CategoriesApi";
        private readonly HttpClient _httpClient;

        public CategoryService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(ApiBaseUrl)
            };

            var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);
            var resultAsString = await httpResponseMessage.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(resultAsString)!.ToList();

            return categories;
        }
    }
}