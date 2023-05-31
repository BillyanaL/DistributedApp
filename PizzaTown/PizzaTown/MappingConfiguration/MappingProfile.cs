using AutoMapper;
using PizzaTown.Data.Models;
using PizzaTown.Models.Meals;

namespace PizzaTown.MappingConfiguration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Meal, MealFormModel>()
                .ReverseMap();
            CreateMap<Meal, MealListingModel>()
                .ReverseMap();
            CreateMap<Meal, MealDetailedModel>()
                .ForMember(m => m.CategoryName, cfg => cfg.MapFrom(x => x.Category.Name))
                .ReverseMap();
        }
    }
}