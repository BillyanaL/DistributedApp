using AutoMapper;
using PizzaTown.Data.Models;
using PizzaTown.Models;

namespace PizzaTown.MappingConfiguration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Meal, MealFormModel>()
                .ReverseMap();
        }
    }
}