using AutoMapper;
using SupermarketWebAPI.Domain.Models;
using SupermarketWebAPI.Domain.Models.Queries;
using SupermarketWebAPI.Extensions;
using SupermarketWebAPI.Resources;

namespace SupermarketWebAPI.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Category, CategoryResource>();

            CreateMap<Product, ProductResource>()
                .ForMember(src => src.UnitOfMeasurement,
                           opt => opt.MapFrom(src => src.UnitOfMeasurement.ToDescriptionString()));

            CreateMap<QueryResult<Product>, QueryResultResource<ProductResource>>();
        }
    }
}
