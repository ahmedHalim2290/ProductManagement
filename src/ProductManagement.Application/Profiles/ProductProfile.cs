using AutoMapper;
using ProductManagement.Application.DTOs;
using ProductManagement.Core.Entities;

namespace ProductManagement.Application.Profiles;

public class ProductProfile : Profile {
    public ProductProfile()
    {
        CreateMap<Product, ProductRequestDto>().ReverseMap();
        CreateMap<Product, ProductResponseDto>()
    .ForMember(dest => dest.QuantityPerUnitName,
               opt => opt.MapFrom(src => src.QuantityPerUnit.ToString()));
    }
}