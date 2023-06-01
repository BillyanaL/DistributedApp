namespace PizzaTown.Models.Meals
{
    public class MealDetailedModel : MealListingModel
    {
        public string CategoryName { get; set; } = null!;
        public string AuthorId { get; set; } = null!;
    }
}