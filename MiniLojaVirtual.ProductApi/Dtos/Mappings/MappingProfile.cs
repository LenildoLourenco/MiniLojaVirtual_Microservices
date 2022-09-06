using AutoMapper;
using MiniLojaVirtual.ProductApi.Models;

namespace MiniLojaVirtual.ProductApi.Dtos.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Product, ProductDto>().ReverseMap();
    }
}
