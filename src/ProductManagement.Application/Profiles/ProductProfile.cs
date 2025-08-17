using AutoMapper;
using ProductManagement.Application.DTOs;
using ProductManagement.Core.Entities;

namespace ProductManagement.Application.Profiles;

public class ProductProfile : Profile {
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
    }
}