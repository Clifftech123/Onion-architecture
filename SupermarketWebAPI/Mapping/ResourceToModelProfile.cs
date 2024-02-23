using AutoMapper;
using SupermarketWebAPI.Domain.Models;
using SupermarketWebAPI.Domain.Models.Queries;
using SupermarketWebAPI.Resources;

namespace SupermarketWebAPI.Mapping
{

    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveCategoryResource, CategoryResource>();

            CreateMap<SaveProductResource, Product>()
                .ForMember(src => src.UnitOfMeasurement, opt => opt.MapFrom(src => (UnitOfMeasurement)src.UnitOfMeasurement));

            CreateMap<ProductsQueryResource, ProductsQuery>();
        }
    }


}
