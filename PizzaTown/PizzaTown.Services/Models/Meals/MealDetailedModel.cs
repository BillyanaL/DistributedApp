using Newtonsoft.Json;

namespace PizzaTown.Services.Models.Meals
{
    public class MealDetailedModel : MealListingModel
    {
        [JsonProperty("category.name")]
        public string CategoryName { get; set; } = null!;
        public string AuthorId { get; set; } = null!;
    }
}